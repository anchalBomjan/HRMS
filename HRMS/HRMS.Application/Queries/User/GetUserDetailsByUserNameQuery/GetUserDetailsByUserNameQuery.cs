using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.User.GetUserDetailsByUserNameQuery
{
    public  class GetUserDetailsByUserNameQuery:IRequest<UserDetailsResponseDTO>
    {
        public string UserName { get; set; }    
    }
}
