using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.StockAssign.Create
{
    public class ClassCreateStockAssignmentValidator : AbstractValidator<CreateStockAssignmentCommand>
    {
        public ClassCreateStockAssignmentValidator()
        {
            RuleFor(x => x.EmployeeId).GreaterThan(0);
            RuleFor(x => x.StockId).GreaterThan(0);
            RuleFor(x => x.AssignedQuantity).GreaterThan(0);
        }

    }
}
