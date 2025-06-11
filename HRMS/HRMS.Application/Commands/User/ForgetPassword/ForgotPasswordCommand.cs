using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.User.ForgetPassword
{
    public  class ForgotPasswordCommand:IRequest<string>
    {

        public string Email { get; set; }
        public ForgotPasswordCommand( string email)
        {
            Email = email;
            
        }

    }
}
