using HRMS.Application.Commands.User.Create;
using HRMS.Application.Common.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestCaseDemo.Test.Command.User
{
    public class CreateUserCommandHandlerTests

    {

        //if a command handler has no external dependencies  you can test it directly without mocking anything but in this cas i mock IIDentitiyservices

        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly CreateUserCommandHandler _handler;


        public CreateUserCommandHandlerTests()
        {
            _identityServiceMock = new Mock<IIdentityService>();
            _handler = new CreateUserCommandHandler(_identityServiceMock.Object);
        }


    
        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public async Task Handle_Should_Return_Correct_Result_Based_On_Service_Response(bool serviceResult, int expected)
        {
            var command = new CreateUserCommand
            {
                FullName = "Test User",
                UserName = "testuser",
                Email = "test@example.com",
                Password = "Password123",
                ConfirmationPassword = "Password123"
            };

            // Setup mock to return a tuple (success flag and userId or null)

            _identityServiceMock
                .Setup(s => s.CreateUserAsync(command.UserName, command.Password, command.Email, command.FullName))
                .ReturnsAsync((serviceResult, serviceResult ? "userid" : null));

            // Act: call the handler's Handle method

            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert: check if the handler returned expected value (1 for success, 0 for failure)

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Service_Throws()
        {
            var command = new CreateUserCommand
            {
                FullName = "Test User",
                UserName = "testuser",
                Email = "test@example.com",
                Password = "Password123",
                ConfirmationPassword = "Password123"
            };

            _identityServiceMock
                .Setup(s => s.CreateUserAsync(command.UserName, command.Password, command.Email, command.FullName))
                .ThrowsAsync(new Exception("Service failure"));

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
