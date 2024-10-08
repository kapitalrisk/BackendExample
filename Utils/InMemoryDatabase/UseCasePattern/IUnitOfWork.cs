﻿using System;
using System.Data;

namespace InMemoryDatabase.UseCasePattern
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        void Begin();
        void Commit();
        void Rollback();
    }
}
