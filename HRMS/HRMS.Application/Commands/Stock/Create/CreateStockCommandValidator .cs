//using FluentValidation;
//using HRMS.Application.Commands.User.Create;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HRMS.Application.Commands.stock.Create
//{
//    public  class CreateStockCommandValidator: AbstractValidator<CreateUserCommand>
//    {
//        public CreateStockCommandValidator()
//        {
//            RuleFor(x => x.Name)
//                .NotEmpty().WithMessage("Name is required.");

//            RuleFor(x => x.Type)
//                .IsInEnum().WithMessage("Invalid stock type."); // ensures it matches the enum

//            RuleFor(x => x.Quantity)
//                .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");
//        }

//    }
//}
