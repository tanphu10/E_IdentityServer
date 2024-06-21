using AutoMapper;
using EMicroservices.IDP.Common.Domains;
using EMicroservices.IDP.Entities;
using EMicroservices.IDP.Infrastructure.Common.Repositories;
using EMicroservices.IDP.Infrastructure.Domains;
using EMicroservices.IDP.Infrastructure.Entities;
using EMicroservices.IDP.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace EMicroservices.IDP.Common.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityContext _dbContext;
        private readonly IMapper _mapper;
        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        private readonly Lazy<IPermissionRepository> _permissionRepository;
        public RepositoryManager(IUnitOfWork unitOfWork, IdentityContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            UserManager = userManager;
            RoleManager = roleManager;
            _mapper = mapper;
            _permissionRepository = new Lazy<IPermissionRepository>(() => new PermissionRepository(_dbContext, _unitOfWork,UserManager,_mapper));
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
