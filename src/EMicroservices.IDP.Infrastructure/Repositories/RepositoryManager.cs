using EMicroservice.IDP.Common.Domains;
using EMicroservice.IDP.Entities;
using EMicroservice.IDP.Infrastructure.Common.Repositories;
using EMicroservice.IDP.Infrastructure.Domains;
using EMicroservice.IDP.Infrastructure.Entities;
using EMicroservice.IDP.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace EMicroservice.IDP.Common.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityContext _dbContext;
        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        private readonly Lazy<IPermissionRepository> _permissionRepository;
        public RepositoryManager(IUnitOfWork unitOfWork, IdentityContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            UserManager = userManager;
            RoleManager = roleManager;
            _permissionRepository = new Lazy<IPermissionRepository>(() => new PermissionRepository(_dbContext, _unitOfWork));
        }
        public IPermissionRepository Permission => _permissionRepository.Value;
        public Task<IDbContextTransaction> BeginTransactionAsync()
        => _dbContext.Database.BeginTransactionAsync();

        public Task EndTransactionAsync()
       => _dbContext.Database.CommitTransactionAsync();

        public void RollbackTransaction()
       => _dbContext.Database.RollbackTransactionAsync();
        public Task<int> SaveAsync()
        => _unitOfWork.CommitAsync();
    }
}
