using AgroBarn.Domain.ApiModels.V1.Result;
using AgroBarn.Domain.ApiModels.V1.Response;
using AgroBarn.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgroBarn.Domain.Supervisor.V1
{
    public partial class AgroBarnSupervisor
    {
        public async Task<List<UserResult>> GetAllUserAsync()
        {
            List<UserResult> userList = new List<UserResult>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                List<RoleResponse> roles = new List<RoleResponse>();

                foreach (var nameRole in userRoles)
                {
                    var role = await _roleManager.FindByNameAsync(nameRole);
                    RoleResponse userRole = _mapper.Map<RoleResponse>(role);
                    roles.Add(userRole);
                }

                PeopleDto peopleData = await _peopleRepository.GetByUserIdAsync(user.Id);
                UserResult userResponse = _mapper.Map<UserResult>(peopleData);
                userResponse.Success = true;
                userResponse.PeopleId = peopleData.Id;
                userResponse.PhoneNumber = user.PhoneNumber;
                userResponse.UserRoles = roles;

                userList.Add(userResponse);
            }

            return userList;
        }

        public async Task<UserResult> GetUserByIdAsync(int userId)
        {
            try
            {
                //User data
                var user = await _userManager.FindByIdAsync(userId.ToString());
                PeopleDto peopleData = await _peopleRepository.GetByUserIdAsync(userId);

                if (user == null)
                    return await ResponseErrorUserResult("not-found", 404);

                //People data
                if (peopleData == null)
                    return await ResponseErrorUserResult("user-people-not-found", 404);

                //User roles name
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Count == 0)
                    return await ResponseErrorUserResult("user-roles-not-found", 404);

                //User roles data
                List<RoleResponse> roles = new List<RoleResponse>();
                foreach (var nameRole in userRoles)
                {
                    var role = await _roleManager.FindByNameAsync(nameRole);
                    RoleResponse rolResponse = _mapper.Map<RoleResponse>(role);
                    roles.Add(rolResponse);
                }

                //Add data of User
                UserResult userResponse = _mapper.Map<UserResult>(peopleData);
                userResponse.Success = true;
                userResponse.PeopleId = peopleData.Id;
                userResponse.PhoneNumber = user.PhoneNumber;
                userResponse.UserRoles = roles;

                return userResponse;
            }
            catch (Exception)
            {
                return await ResponseErrorUserResult("internal-server-error", 500);
            }
        }

        private async Task<UserResult> ResponseErrorUserResult(string code, int httpCode)
        {
            MessageDto message = await _messageRepository.GetByCodeAsync(code);
            return new UserResult
            {
                Success = false,
                CodeError = httpCode,
                Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        Code = message.Code,
                        Message = message.Description
                    }
                }
            };
        }
    }
}
