using EMicroservice.IDP.Common.Domains;
using EMicroservice.IDP.Common.Domains.Repository;
using EMicroservice.IDP.Entities;
using EMicroservice.IDP.Persistence;

namespace EMicroservice.IDP.Common.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission, long>, IPermissionRepository
    {

        public PermissionRepository(IdentityContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public Task<IEnumerable<Permission>> GetPermissionByRole(string roleId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermissionsByRoleId(string roleId, IEnumerable<Permission> permissionCollection, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
