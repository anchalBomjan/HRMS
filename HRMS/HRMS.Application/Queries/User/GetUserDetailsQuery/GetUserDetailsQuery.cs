using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.User.GetUserDetailsQuery
{

    public class GetUserDetailsQuery : IRequest<UserDetailsResponseDTO>
    {
        public string UserId { get; set; }
    }
}
