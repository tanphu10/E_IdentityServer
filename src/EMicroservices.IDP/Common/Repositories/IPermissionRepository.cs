using EMicroservice.IDP.Common.Domains.Repository;
using EMicroservice.IDP.Entities;

namespace EMicroservice.IDP.Common.Repositories
{
    public interface IPermissionRepository:IRepositoryBase<Permission,long>
    {
        Task<IEnumerable<Permission>> GetPermissionByRole(string roleId, bool trackChanges);
        void UpdatePermissionsByRoleId(string roleId, IEnumerable<Permission> permissionCollection, bool trackChanges); 
    }
}
