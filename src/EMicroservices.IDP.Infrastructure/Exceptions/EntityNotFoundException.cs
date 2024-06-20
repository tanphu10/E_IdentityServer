using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMicroservices.IDP.Infrastructure.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException() : base("Entity was not found.")
        {
        }

    }
}
