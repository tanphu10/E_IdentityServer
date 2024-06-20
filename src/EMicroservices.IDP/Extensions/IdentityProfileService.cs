using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using EMicroservice.IDP.Common;
using EMicroservice.IDP.Entities;
using EMicroservice.IDP.Infrastructure.Common;
using EMicroservice.IDP.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EMicroservice.IDP.Extensions
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _claimsFatory;
        private readonly UserManager<User> _userManager;

        public IdentityProfileService(IUserClaimsPrincipalFactory<User> claimsFatory,UserManager<User> userManager)
        {
            _userManager = userManager;
            _claimsFatory = claimsFatory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                throw new ArgumentException("User Id not found");
            }
            var principal = await _claimsFatory.CreateAsync(user);
            var claims = principal.Claims.ToList();
            var roles = await _userManager.GetRolesAsync(user);

            //Add more claims like this
            claims.Add(new Claim(SystemConstants.Claims.FirstName, user.FirstName));
            claims.Add(new Claim(SystemConstants.Claims.LastName, user.LastName));
            claims.Add(new Claim(SystemConstants.Claims.UserName, user.UserName));
            claims.Add(new Claim(SystemConstants.Claims.UserId, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(SystemConstants.Claims.Roles, string.Join(";", roles)));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
