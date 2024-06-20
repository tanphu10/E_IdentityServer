using Dapper;
using EMicroservice.IDP.Common.Domains.Repository;
using EMicroservice.IDP.Infrastructure.Common.Repositories;
using EMicroservice.IDP.Infrastructure.Domains;
using EMicroservice.IDP.Infrastructure.Entities;
using EMicroservice.IDP.Persistence;
using EMicroservices.IDP.Infrastructure.ViewModel;
using System.Data;

namespace EMicroservice.IDP.Common.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission, long>, IPermissionRepository
    {

        public PermissionRepository(IdentityContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<PermissionViewModel?> CreatePermission(string roleId, PermissionAddModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@roleId", roleId, DbType.String);
            parameters.Add("@function", model.Function, DbType.String);
            parameters.Add("@command", model.Command, DbType.String);
            parameters.Add("@newID", dbType: DbType.Int64, direction: ParameterDirection.Output);
            var result = await ExecuteAsync("Create_Permission", parameters);

            if (result <= 0) return null;
            var newId = parameters.Get<long>("@newID");
            return new PermissionViewModel
            {
                Id = newId,
                Function = model.Function,
                Command = model.Command,
                RoleId = roleId
            };
        }

        public Task DeletePermission(string roleId, string function, string command)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@roleId", roleId, DbType.String);
            parameters.Add("@function", function, DbType.String);
            parameters.Add("@command", command, DbType.String);
            return ExecuteAsync("Delete_Permission", parameters);


        }

        public async Task<IReadOnlyList<PermissionViewModel>> GetPermissionByRole(string roleId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@roleId", roleId);
            var result = await QueryAsync<PermissionViewModel>("Get_Permission_ByRoleId", parameters);
            return result;

        }

        public Task UpdatePermissionsByRoleId(string roleId, IEnumerable<PermissionAddModel> permissionCollection)
        {
            var dt = new DataTable();
            dt.Columns.Add("RoleId", typeof(string));
            dt.Columns.Add("Function", typeof(string));
            dt.Columns.Add("Command", typeof(string));
            foreach (var item in permissionCollection)
            {
                dt.Rows.Add(roleId, item.Function, item.Command);
                
            }
            var parameters = new DynamicParameters();
            parameters.Add("@roleId", roleId, DbType.String);
            parameters.Add("@permissions", dt.AsTableValuedParameter("dbo.Permission"));
            return ExecuteAsync("Update_Permissions_ByRole", parameters);
        }
    }
}
