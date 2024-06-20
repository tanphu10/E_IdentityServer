using EMicroservice.IDP.Infrastructure.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMicroservices.IDP.Infrastructure.ViewModel
{
    public class PermissionViewModel:EntityBase<long>
    {
        public string Function { get; set; }
        public string RoleId { get; set; }
        public string Command { get; set; }
    }
}
