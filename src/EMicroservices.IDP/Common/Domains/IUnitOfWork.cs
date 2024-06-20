﻿namespace EMicroservice.IDP.Common.Domains
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
