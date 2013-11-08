using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.Objects;

namespace  Managers.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsInTransaction { get; }

        IUnitOfWorkResult SaveChanges();

        //IUnitOfWorkResult SaveChanges(SaveOptions saveOptions);

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void RollBackTransaction();

        IUnitOfWorkResult CommitTransaction();
    }
}
