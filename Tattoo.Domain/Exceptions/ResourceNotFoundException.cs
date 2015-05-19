using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tattoo.Domain.Exceptions
{
    public class ResourceNotFoundException :Exception
    {
        public ResourceNotFoundException()
            : base("Resource not found")
        {

        }

        public ResourceNotFoundException(string message) : base(message) { }

    }
}
