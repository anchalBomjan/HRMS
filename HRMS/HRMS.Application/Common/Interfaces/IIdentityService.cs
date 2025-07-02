using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Common.Interfaces
{
    public  interface IIdentityService
    {

        //Use section
        //Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email, string fullName, IList<string> roles
        //
        Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email, string fullName);

        Task<bool> SigninUserAsync(string userName, string password);


        Task<string> GetUserIdAsync(string userName);
        Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync();
        Task<List<(string id,  string userName, string fullName, string email, IList<string> roles)>> GetAllUsersDetailsAsync();


        Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string Id);
        Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);


        Task<string> GetUserNameAsync(string userId);

        Task<bool> DeleteUserAsync(string userId);

        Task<bool> IsUniqueUserName(string userName);
        Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles);






        //Roles  --- sections
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<List<(string id, string roleName)>> GetRolesAsync();
        Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        Task<bool> UpdateRole(string id, string roleName);


        //User's Role Section

        Task<bool> IsInRoleAsync(string userId, string role);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task<bool> AssignUserToRole(string userName, IList<string> roles);
        Task<bool> UpdateUsersRole(string userName, IList<string> userRole);



        ///////
        ///\
        ///------------------------For forgetPassword and recovery
        ///
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

    }
}
