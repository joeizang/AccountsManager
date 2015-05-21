using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccountManager.Web.Controllers;
using NSubstitute;
using AccountManager.Domain.Abstractions;
using AccountManager.Domain.Entities;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace AccountManager.Tests.Controllers
{
    [TestClass]
    public class IncomeControllerTests
    {
        [TestMethod]
        public async Task IndexActionLoadsDataAndReturnsView()
        {
            //Arrange
            //var fake = Substitute.For<IDbSet<Income>>();
            var data = new List<Accounts>{ new Accounts{ Bank = new Bank{ BankName="Zenith Bank"}, AccountBalance=1200000M, AccountName="Joseph Izang",
                                                        AccountTypes= AccountTypes.Current}, new Accounts{ AccountTypes= AccountTypes.Current,
                                                        Bank = new Bank{ BankName="GT Bank"}, 
                                                        AccountName="Joseph A. Izang", AccountBalance=1299000M}}.AsQueryable();

            var acct = Substitute.For<IDbSet<Accounts>, IDbAsyncEnumerable<Accounts>>().Initialize(data);

            var context = Substitute.For<IAccountManagerdb>();
            context.Accounts.Returns(acct);
            var controller = new AccountsController(context);

            var result = await controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
            
        }
    }

    public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
       where TEntity : class
    {
        ObservableCollection<TEntity> _data;
        IQueryable _query;

        public TestDbSet()
        {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public override TEntity Add(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Remove(TEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
        }
    }

    internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }

    public static class ExtensionMethods
    {
        public static IDbSet<T> Initialize<T>(this IDbSet<T> db,
            IQueryable<T> data) where T : class
        {
            db.Provider.Returns(data.Provider);
            db.Expression.Returns(data.Expression);
            db.ElementType.Returns(data.ElementType);
            db.GetEnumerator().Returns(data.GetEnumerator());

            if (db is IDbAsyncEnumerable)
            {
                ((IDbAsyncEnumerable<T>)db).GetAsyncEnumerator()
                    .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));
                db.Provider.Returns(new TestDbAsyncQueryProvider<T>(data.Provider));
            }

            return db;
        }
    }
}
