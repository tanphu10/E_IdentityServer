using EMicroservice.IDP.Common.Repositories;
using EMicroservices.IDP.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EMicroservices.IDP.Presentation.Controllers
{
    [Route("api/[controller]/roles/{roleId}")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public PermissionsController(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPermission(string roleId)
        {
            var result = await _repository.Permission.GetPermissionByRole(roleId);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> createPermission(string roleId, PermissionAddModel model)
        {
            var result = await _repository.Permission.CreatePermission(roleId, model);
            return result != null ? Ok(result) : NoContent();
        }
        [HttpDelete("function/{function}/command/{command}")]
        [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePermission(string roleId, [Required] string function, string command)
        {
            await _repository.Permission.DeletePermission(roleId, function, command);
            return NoContent();
        }
        [HttpPut("update-permissions")]
        [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePermission(string roleId, [FromBody]IEnumerable<PermissionAddModel> permissions)
        {
            await _repository.Permission.UpdatePermissionsByRoleId(roleId, permissions);
            return NoContent();
        }
    }
}
