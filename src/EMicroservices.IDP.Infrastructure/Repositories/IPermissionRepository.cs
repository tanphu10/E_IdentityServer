using EMicroservices.IDP.Infrastructure.Domains.Repository;
using EMicroservices.IDP.Infrastructure.Entities;
using EMicroservices.IDP.Infrastructure.ViewModel;

namespace EMicroservices.IDP.Infrastructure.Common.Repositories
{
    public interface IPermissionRepository:IRepositoryBase<Permission,long>
    {
        Task<IReadOnlyList<PermissionViewModel>> GetPermissionByRole(string roleId);
        Task<PermissionViewModel?> CreatePermission(string roleId, PermissionAddModel model);
        Task DeletePermission(string roleId, string function, string command);
        Task UpdatePermissionsByRoleId(string roleId, IEnumerable<PermissionAddModel> permissionCollection);
        Task<IEnumerable<PermissionViewModel>> GetPerrmissionByUser(User user);
    }
}
