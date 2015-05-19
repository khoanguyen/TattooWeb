using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tattoo.Domain.Exceptions
{
    public class CanNotDeleteException : Exception
    {
        public CanNotDeleteException() : base("Can not delete resource") { }

        public CanNotDeleteException(string message) : base(message) { }
    }
}
