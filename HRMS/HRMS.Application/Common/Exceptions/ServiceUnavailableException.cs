using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Common.Exceptions
{
    public class BadGatewayException : Exception
    {
        public BadGatewayException(string message = "Bad Gateway") : base(message) { }
    }
}
