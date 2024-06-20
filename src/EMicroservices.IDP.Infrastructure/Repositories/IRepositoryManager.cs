using EMicroservice.IDP.Infrastructure.Common.Repositories;
using EMicroservice.IDP.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace EMicroservice.IDP.Common.Repositories
{
    public interface IRepositoryManager
    {
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IPermissionRepository Permission { get; }
        Task<int> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task EndTransactionAsync();
        void RollbackTransaction();



    }
}
