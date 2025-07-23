//////using HRMS.Application.Commands.User.Create;
//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;
//////using System.Threading.Tasks;
//////using Xunit;

//////namespace HRMS.UnitTest.Command.User
//////{
//////    public  class CreateUserCommandValidatorTests
//////    {

       

//////        private readonly CreateUserCommandValidator _validator;

//////        public CreateUserCommandValidatorTests()
//////        {
//////            _validator = new CreateUserCommandValidator();
//////        }

//////        [Theory]
//////        [InlineData(null)]
//////        [InlineData("")]
//////        public void Should_Have_Error_When_Email_Is_Empty(string email)
//////        {
//////            var model = new CreateUserCommand { Email = email };
//////            var result = _validator.Validate(model);

//////            Assert.False(result.IsValid);
//////            Assert.Contains(result.Errors, e =>
//////                e.PropertyName == nameof(model.Email) &&
//////                e.ErrorMessage == "Email is required.");
//////        }

//////        [Theory]
//////        [InlineData("invalid-email")]
//////        [InlineData("abc@")]
//////        [InlineData("abc.com")]
//////        public void Should_Have_Error_When_Email_Is_Invalid(string email)
//////        {
//////            var model = new CreateUserCommand { Email = email };
//////            var result = _validator.Validate(model);

//////            Assert.False(result.IsValid);
//////            Assert.Contains(result.Errors, e =>
//////                e.PropertyName == nameof(model.Email) &&
//////                e.ErrorMessage == "Invalid email format.");
//////        }

//////        [Theory]
//////        [InlineData("user@example.com")]
//////        [InlineData("valid.email@domain.co")]
//////        public void Should_Not_Have_Error_When_Email_Is_Valid(string email)
//////        {
//////            var model = new CreateUserCommand { Email = email };
//////            var result = _validator.Validate(model);

//////            Assert.True(result.IsValid);
//////        }
//////    }
//////}

