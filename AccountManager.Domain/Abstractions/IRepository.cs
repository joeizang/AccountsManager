using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain.Entities
{
    /// <summary>
    /// Interface to hold the most common operations that every entity be able to do.
    /// The T is for any class which should signify the type that will work with this 
    /// interface
    /// </summary>
    public interface IRepository<T> : IDisposable where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        IQueryable<T> Query(Expression<Func<T,bool>> predicatename);
        T GetById(int id);
        Task<int> CommitAsync();
        //int Saves();
        void Dispose();

    }
}
