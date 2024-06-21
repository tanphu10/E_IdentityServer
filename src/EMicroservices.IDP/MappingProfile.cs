using AutoMapper;
using EMicroservices.IDP.Infrastructure.Entities;
using EMicroservices.IDP.Infrastructure.ViewModel;

namespace EMicroservices.IDP
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Permission, PermissionViewModel >();
        }
    }
}
