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
        public User CurrentUser { get; set; }

        public static DbContext _ctx;
        public static DbContext CTX
        {
            get
            {
                if (_ctx == null)
                    _ctx = new GetTaxiEntities();

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
