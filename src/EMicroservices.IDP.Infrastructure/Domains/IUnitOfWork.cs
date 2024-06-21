namespace EMicroservices.IDP.Infrastructure.Domains
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
