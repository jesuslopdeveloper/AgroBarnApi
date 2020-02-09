using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Response;
using AgroBarn.Domain.ApiModels.V1.Result;
using AgroBarn.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgroBarn.Domain.Identity
{
    public partial class IdentityService
    {
        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest newUser)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(newUser.Email);

                if (existingUser != null)
                    return await ResponseError("identity-email-already-in-use");

                ApplicationUser aspNetUser = new ApplicationUser
                {
                    Email = newUser.Email,
                    UserName = newUser.Email,
                    PhoneNumber = newUser.PhoneNumber,
                    Status = 1
                };

                var createdUser = await _userManager.CreateAsync(aspNetUser, newUser.Password);
                if (!createdUser.Succeeded)
                    return ResponseErrorUser(createdUser);

                //Adding the role to new user
                await _userManager.AddToRoleAsync(aspNetUser, "User");

                PeopleDto newPeople = new PeopleDto
                {
                    UserId = aspNetUser.Id,
                    Name = newUser.Name,
                    FirstSurname = newUser.FirstSurname,
                    SecondSurname = newUser.SecondSurname,
                    Status = 1,
                    UserCreate = aspNetUser.Id,
                    DateCreate = DateTime.Now
                };

                bool createdPeople = await _peopleRepository.AddAsync(newPeople);

                if (!createdPeople)
                    return await ResponseError("identity-person-data-not-saved");

                return await GenerateAuthenticationResultForUserAsync(aspNetUser);
            }
            catch (Exception)
            {
                return await ResponseError("internal-server-error");
            }
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return await ResponseError("identity-user-not-found");

                var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

                if (!userHasValidPassword)
                    return await ResponseError("identity-wrong-password");

                return await GenerateAuthenticationResultForUserAsync(user);
            }
            catch (Exception)
            {
                return await ResponseError("internal-server-error");
            }
        }
        
        public async Task<AuthenticationResult> RefreshTokenAsync(string token, Guid refreshToken)
        {
            try
            {
                var validatedToken = GetPrincipalFromToken(token);

                //Validate Token
                if (validatedToken == null)
                    return await ResponseError("identity-token-invalid");

                var expiryDateUnix =
                    long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(expiryDateUnix);

                //Validate DateTime
                if (expiryDateTimeUtc > DateTime.UtcNow)
                    return await ResponseError("identity-token-not-expired");

                var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                //Get token from database
                RefreshToken storedRefreshToken = await _refreshTokenRepository.GetByToken(refreshToken);

                if (storedRefreshToken == null)
                    return await ResponseError("identity-refresh-token-not-exist");

                if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                    return await ResponseError("identity-refresh-token-expired");

                if (storedRefreshToken.Invalidated)
                    return await ResponseError("identity-refresh-token-invalidated");

                if (storedRefreshToken.Used)
                    return await ResponseError("identity-refresh-token-used");

                if (storedRefreshToken.JwtId != jti)
                    return await ResponseError("identity-refresh-token-not-match");

                storedRefreshToken.Used = true;
                await _refreshTokenRepository.UpdateAsync(storedRefreshToken);

                var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
                return await GenerateAuthenticationResultForUserAsync(user);
            }
            catch (Exception)
            {
                return await ResponseError("internal-server-error");
            }
        }

        private async Task<AuthenticationResult> ResponseError(string code)
        {
            MessageDto message = await _messageRepository.GetByCodeAsync(code);
            return new AuthenticationResult
            {
                Success = false,
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

        private AuthenticationResult ResponseErrorUser(IdentityResult result)
        {
            List<ErrorModel> errors = new List<ErrorModel>();
            foreach (var error in result.Errors)
            {
                errors.Add(new ErrorModel
                {
                    Code = error.Code,
                    Message = error.Description
                });
            }

            return new AuthenticationResult
            {
                Success = false,
                Errors = errors
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;
                
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            //Claims Token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id.ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            //Roles user
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) 
                    continue;

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim)) 
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var now = DateTime.UtcNow;

            //Configure Securiry Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = now.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Create refresh Token
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = now,
                ExpiryDate = now.AddMinutes(_jwtSettings.RefreshTokenLifeTime)
            };

            refreshToken = await _refreshTokenRepository.AddAsync(refreshToken);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
