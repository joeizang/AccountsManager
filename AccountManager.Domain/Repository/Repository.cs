using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Domain.Entities;
using System.Data.Entity;
using AccountManager.Domain.Contexts;

namespace AccountManager.Domain.Repository
{
    /// <summary>
    /// Generic Repository class that allows the efficient consumption of IRepository Interface.
    /// This obviously implements the Generic Repository Pattern 
    /// </summary>
    /// <typeparam name="T"> T is any type that is an entity since solution is using Entity Framework 6</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AccountManagerdb dbCntx;
        private AccountManagerdb set { get; set; }

        public Repository(AccountManagerdb db)
        {
            dbCntx = db;
        }

        public void Add(T entity)
        {
            dbCntx.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            dbCntx.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            if (set.Entry(entity).State == EntityState.Detached)
            {
                dbCntx.Set<T>().Attach(entity);
                set.Entry(entity).State = EntityState.Modified;
            }
            
        }

        public IEnumerable<T> GetAll()
        {
            return dbCntx.Set<T>();
        }

        public IQueryable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> predicatename)
        {
            return dbCntx.Set<T>().Where(predicatename);
        }

        public T GetById(int id)
        {
            return dbCntx.Set<T>().Find(id);
        }

        public async Task<int> CommitAsync()
        {
            return await dbCntx.SaveChangesAsync();
        }

        public virtual int Saves()
        {
            return dbCntx.SaveChanges();
        }

        public void Dispose()
        {
            dbCntx.Dispose();
        }
    }
}
