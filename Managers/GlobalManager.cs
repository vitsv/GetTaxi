using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;
using Managers.Repository;
using System.Data.Entity;

namespace Managers
{
    public abstract class GlobalManager : IDisposable
    {
        public GlobalManager(bool enableProxy)
        {
            EnableProxy = enableProxy;
        }

        public GlobalManager()
        {
            EnableProxy = true;
        }

        public bool EnableProxy { get; set; }
        public User CurrentUser { get; set; }

        public DbContext _ctx;
        public DbContext CTX
        {
            get
            {
                if (_ctx == null)
                    _ctx = new GetTaxiEntities();
                _ctx.Configuration.ProxyCreationEnabled = EnableProxy;
                _ctx.Configuration.LazyLoadingEnabled = EnableProxy;
                _ctx.Configuration.AutoDetectChangesEnabled = EnableProxy;
                return _ctx;
            }
        }

        public IRepository RepoGeneric
        {
            get
            {
                var ctx = CTX;

                var repository = new GenericRepository(ctx);
                return repository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ctx != null)
                {
                    _ctx.Dispose();
                    _ctx = null;
                }
            }
        }

        public UnitOfWorkResult CreateResultError(string errorMsg)
        {
            return new UnitOfWorkResult
            {
                IsError = true,
                ErrorInfo = new Exception(errorMsg)
            };
        }
    }

}
