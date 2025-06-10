using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.Auth
{
    public  class AuthCommand :IRequest<AuthResponseDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }    
    }
}
