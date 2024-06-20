using System.ComponentModel.DataAnnotations;

namespace EMicroservices.IDP.Infrastructure.ViewModel
{
    public class PermissionAddModel
    {
        [Required]
        public string Function { get; set; }
        [Required]

        public string Command { get; set; }
    }
}
