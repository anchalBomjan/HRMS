
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

        /// <summary>
        /// //------------------------User section------------------------------------
        /// </summary>




        //public async Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email, string fullName, IList<string> roles)
        //{
        //    var user = new ApplicationUser()
        //    {
        //        FullName = fullName,
        //        UserName = userName,
        //        Email = email,

        //    };
        //    var result = await _userManager.CreateAsync(user, password);
        //    if (!result.Succeeded)
        //    {
        //        var failure = result.Errors.Select(e =>
        //        new ValidationFailure(nameof(user), e.Description));


        //    }
        //    var addUserRole = await _userManager.AddToRolesAsync(user, roles);
        //    if (!addUserRole.Succeeded)
        //    {
        //        var failures = addUserRole.Errors
        //            .Select(e => new ValidationFailure(nameof(roles), e.Description));
        //        throw new ValidationException(failures);

        //    }
        //    return (result.Succeeded, user.Id);
        //}


        public async Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email, string fullName)
        {


            var user = new ApplicationUser
            {
                FullName = fullName,
                UserName = userName,
                Email = email
            };

            // Create user
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var failures = result.Errors
                    .Select(e => new ValidationFailure(nameof(user), e.Description));
                throw new ValidationException(failures);
            }

            // Assign default role "User"
            var addRoleResult = await _userManager.AddToRoleAsync(user, "User"); // Or use StaticUserRoles.USER
            if (!addRoleResult.Succeeded)
            {
                var failures = addRoleResult.Errors
                    .Select(e => new ValidationFailure(nameof(user), e.Description));
                throw new ValidationException(failures);
            }

            return (true, user.Id);
        }


        public async Task<bool> IsUniqueUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName) == null;
        }




        public async Task<bool> SigninUserAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            return result.Succeeded;


            // true for presistence Creates a persistent cookie → user stays logged in across browser sessions
            // false lockoutfailure :Disable
        }

        public async Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync()
        {


            var users = await _userManager.Users.Select(x => new
            {
                x.Id,
                x.FullName,
                x.UserName,
                x.Email
            }).ToListAsync();

            return users.Select(user => (user.Id, user.FullName, user.UserName, user.Email)).ToList();
        }


        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User is not found");

            }
            return await _userManager.GetUserIdAsync(user);

        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not Found");

            }
            return await _userManager.GetUserNameAsync(user);

        }


        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User NotFound");



            }
            if (user.UserName == "system" || user.UserName == "Admin")
            {
                throw new Exception(" you cannot delete system or Admin");
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;

        }

       




        /// <summary>
        /// /// ------------------------- Roles section--------------------------------------------
        /// </summary>
        /// 
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


        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var roleDetails = await _roleManager.FindByIdAsync(roleId);
            if (roleDetails == null)
            {
                throw new NotFoundException("Role is  not found");
            }

            if (roleDetails.Name == "Administrator")
            {
                throw new BadRequestException(" You can not delete  Administration Role");

            }

            var result = await _roleManager.DeleteAsync(roleDetails);
            if (!result.Succeeded)
            {
                var failures = result.Errors
                     .Select(e => new ValidationFailure(nameof(roleId), e.Description));
                throw new ValidationException(failures);
            }

            return result.Succeeded;
        }





        public async Task<(string id, string roleName)> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return (role.Id, role.Name);

        }

        public async Task<List<(string id, string roleName)>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToListAsync();

            return roles.Select(role => (role.Id, role.Name)).ToList();
        }

        //******** for forget passwords**********


        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException($"User with email {email} not found.");

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }


        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NotFoundException($"User with email '{email}' not found.");

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                // Map Identity errors to FluentValidation errors
                var failures = result.Errors
                    .Select(e => new ValidationFailure("Password", e.Description)) // "Password" can be changed based on context
                    .ToList();

                throw new ValidationException(failures);
            }

            return true;
        }









        /// <summary>
        /// //------------------- User's Roles Section--------------------------------------------------------------------------
        /// </summary>





        public async Task<bool> UpdateRole(string id, string roleName)
        {
            if (roleName != null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                role.Name = roleName;
                var result = await _roleManager.UpdateAsync(role);
                return result.Succeeded;
            }
            return false;

        }

        public async Task<List<(string id, string userName, string fullName, string email, IList<string> roles)>> GetAllUsersDetailsAsync()
        {
            var users = await _userManager.Users.ToArrayAsync();

            var userDetails = new List<(string id, string userName, string fullName, string email, IList<string> roles)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDetails.Add((user.Id, user.UserName, user.FullName, user.Email, roles));
            }

            return userDetails;
        }
        public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                throw new NotFoundException("user", Id);
            }

            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }

        // this is  handle by only user for profile edit 
        public async Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), id);

            }

            user.FullName = fullName;
            user.Email = email;
            
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;

        }

        public async Task<bool> UpdateUsersRole(string userName, IList<string> usersRole)
        {

            var user = await _userManager.FindByNameAsync(userName);
            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            result = await _userManager.AddToRolesAsync(user, usersRole);


            return result.Succeeded;

        }
        public async Task<bool> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }


            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;

        }


        public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                throw new NotFoundException("User NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);

        }



        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new NotFoundException("User not Found");

            }
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();

        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {                                                                                                                           
                throw new NotFoundException("User not Found");
            }
            return await _userManager.IsInRoleAsync(user, role);
        }

    }
}
