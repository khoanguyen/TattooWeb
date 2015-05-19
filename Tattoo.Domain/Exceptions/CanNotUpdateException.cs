using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tattoo.Domain.Exceptions
{
    public class CanNotUpdateException : Exception
    {
        public CanNotUpdateException() : base("Can not update resource") { }

        public CanNotUpdateException(string message) : base(message) { }
    }
}
