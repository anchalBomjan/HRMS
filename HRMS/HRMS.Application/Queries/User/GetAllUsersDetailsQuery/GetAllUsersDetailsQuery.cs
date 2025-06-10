using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.User.GetAllUsersDetailsQuery
{
    public  class GetAllUsersDetailsQuery:IRequest<List<UserDetailsResponseDTO>>
    {
    }
}
