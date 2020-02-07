using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Result;

using System;
using System.Threading.Tasks;

namespace AgroBarn.Domain.Identity
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest newUser);

        Task<AuthenticationResult> LoginAsync(string email, string password);

        Task<AuthenticationResult> RefreshTokenAsync(string token, Guid refreshToken);
    }
}
