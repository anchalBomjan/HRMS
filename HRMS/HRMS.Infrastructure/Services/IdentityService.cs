
using FluentValidation;
using FluentValidation.Results;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Services
{
    public  class IdentityService:IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly  SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IdentityService(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;


        }

        public async  Task<bool> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
          

            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;

        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                 var failures = result.Errors.Select(e =>
                new ValidationFailure(nameof(roleName), e.Description));
                ///throw new ValidationException(failures);
                      

            }
             return result.Succeeded;

          
        }

        public  async Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email, string fullName, IList<string> roles)
        {
            var user = new ApplicationUser()
            {
                FullName= fullName,
                UserName= userName,
                Email= email,

            };
            var result= await _userManager.CreateAsync(user,password);
            if (!result.Succeeded)
            {
                var failure = result.Errors.Select(e =>
                new ValidationFailure(nameof(user), e.Description));
                

            }
            var addUserRole=await _userManager.AddToRolesAsync(user,roles);
            if (!addUserRole.Succeeded)
            {
                var failures=addUserRole.Errors
                    .Select(e=> new ValidationFailure(nameof(roles), e.Description));
                  throw new ValidationException(failures);

            }
             return(result.Succeeded,user.Id);
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
           var roleDetails= await _roleManager.FindByIdAsync(roleId);
            if(roleDetails == null)
            {
                throw new NotFoundException("Role is  not found");
            }

            if (roleDetails.Name == "Administrator")
            {
                throw new BadRequestException(" You can not delete  Administration Role");

            }

            var result= await _roleManager.DeleteAsync(roleDetails);
            if(!result.Succeeded)
            {
                var failures = result.Errors
                     .Select(e => new ValidationFailure(nameof(roleId), e.Description));
                throw new ValidationException(failures);
            }

             return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user== null) 
            {
                throw new NotFoundException("USer NotFound");
                


            }
            if(user.UserName=="system"|| user.UserName == "Admin")
            {
                throw new Exception(" you cannot delete system or Admin");
            }
            var result= await _userManager.DeleteAsync(user);
            return result.Succeeded;

        }

        public async  Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Select(x => new 
            { 
                x.Id, 
                x.FullName,
                x.UserName,
                x.Email
            }).ToListAsync();

            return users.Select(user => (user.Id, user.FullName,  user.UserName,user.Email)).ToList();
        }

        public  async Task<List<(string id, string fullName, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync()
        {
            var users = await _userManager.Users.ToArrayAsync();

            var userDetails= new List<(string id, string userName, string fullName, string email, IList<string> roles)>();
           
            foreach(var user in users)
            {
                var role= await _userManager.GetRolesAsync(user);
                userDetails.Add((user.Id,user.FullName,user.UserName, user.Email, role));
            }

             return userDetails;
        }

        public Task<(string id, string roleName)> GetRoleByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<(string id, string roleName)>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

        public  async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string Id)
        {
             var user= await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                throw new NotFoundException("user", Id);
            }

            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }

        public  async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName)
        {
            var user= await _userManager.Users.FirstOrDefaultAsync(x=> x.UserName==userName);

            if(user== null)
            {
                throw new NotFoundException("User NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
            
        }

        public Task<string> GetUserIdAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetUserRolesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUniqueUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SigninUserAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRole(string id, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUsersRole(string userName, IList<string> userRole)
        {
            throw new NotImplementedException();
        }
    }
}
