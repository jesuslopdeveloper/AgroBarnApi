using AgroBarn.Domain.ApiModels.V1.Others;
using AgroBarn.Domain.Entities;
using AgroBarn.Domain.Repositories.V1;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AgroBarn.Domain.Identity
{
    public partial class IdentityService : IIdentityService
    {
        //Custom Manager Identity
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        //Repositories
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IMessageRepository _messageRepository;

        public IdentityService()
        {
        }

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters,
            IRefreshTokenRepository refreshTokenRepository,
            IPeopleRepository peopleRepository,
            IMessageRepository messageRepository
        )
        {
            //Manager Token
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;

            //Repositories
            _refreshTokenRepository = refreshTokenRepository;
            _peopleRepository = peopleRepository;
            _messageRepository = messageRepository;
        }
    }
}
