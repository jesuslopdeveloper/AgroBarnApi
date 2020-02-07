using AgroBarn.Domain.Identity;
using AgroBarn.API.Contracts.V1;
using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Response;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AgroBarn.API.Controllers.V1.Identity
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Registra un nuevo usuario
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<ActionResult> Register([FromBody] UserRegistrationRequest newUser)
        {
            var authResponse = await _identityService.RegisterAsync(newUser);

            if (!authResponse.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Autentica un usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Actualiza el token cuando éste se ha caducado a traves del Refresh Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}