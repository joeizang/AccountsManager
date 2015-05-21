using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AccountManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using MySql.Data.Entity;
using AccountManager.Domain.Abstractions;
using System.Data.Entity.Infrastructure;

namespace AccountManager.Domain.Contexts
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AccountManagerdb : DbContext, IAccountManagerdb
    {
        /// <summary>
        /// Create constructor and supply name of connection string from web.config file.
        /// Uses DbConfiguration attribute to decorate DbContext class to setup class for 
        /// MySql Peculiarities
        /// </summary>
        public AccountManagerdb()
            : base("LocalMySqlServer") //see web.config
        {

        }
        public virtual IDbSet<Accounts> Accounts { get; set; }
        public virtual IDbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public virtual IDbSet<Expense> Expense { get; set; }
        public virtual IDbSet<IncomeCategory> IncomeCategory { get; set; }
        public virtual IDbSet<Income> Income { get; set; }
        public virtual IDbSet<Bank> Bank { get; set; }
        public virtual IDbSet<Payer> Payer { get; set; }
        public virtual IDbSet<Receiver> Receiver { get; set; }
        public virtual IDbSet<Tithe> Tithe { get; set; }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public DbEntityEntry Entry(object o)
        {
            return base.Entry(o);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        
    }
}
