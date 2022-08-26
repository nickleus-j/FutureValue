using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.Domain.Exceptions
{
    public class InvalidBoundsException:Exception
    {
        public override string Message => "Upper Bound value  must never be lower than lower bound value";
    }
}
