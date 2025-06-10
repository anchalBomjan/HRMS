using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.Role.GetRoleByIdQuery
{
    public  class GetRoleByIdQuery:IRequest<RoleResponseDTO>
    {
        public string RoleId { get; set; }

    }
}
