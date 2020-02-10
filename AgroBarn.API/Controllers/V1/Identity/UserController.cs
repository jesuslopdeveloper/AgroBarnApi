using AgroBarn.Domain.Supervisor.V1;
using AgroBarn.API.Contracts.V1;
using AgroBarn.Domain.ApiModels.V1.Result;
using AgroBarn.Domain.ApiModels.V1.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AgroBarn.API.Controllers.V1.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAgroBarnSupervisor _agroBarnSupervisor;
        private readonly IMapper _mapper;

        public UserController(
            IAgroBarnSupervisor agroBarnSupervisor,
            IMapper mapper
        )
        {
            _agroBarnSupervisor = agroBarnSupervisor;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene la lista de los usuarios registrados
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Users.GetAll)]
        public async Task<ActionResult> GetAllUser()
        {
            List<UserResult> response = await _agroBarnSupervisor.GetAllUserAsync();

            List<UserResponse> users = _mapper.Map<List<UserResponse>>(response);
            return Ok(users);
        }

        /// <summary>
        /// Obtiene un usuario a través de su ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Users.GetById)]
        public async Task<ActionResult> GetUserById([FromRoute]int userId)
        {
            UserResult response = await _agroBarnSupervisor.GetUserByIdAsync(userId);

            if (!response.Success)
            {
                return ResponseErrorCode(response);
            }

            UserResponse userResponse = _mapper.Map<UserResponse>(response);
            return Ok(userResponse);
        }

        private ActionResult ResponseErrorCode(UserResult response)
        {
            switch (response.CodeError)
            {
                case 400:
                    return BadRequest(new ErrorResponse
                    {
                        Errors = response.Errors
                    });
                case 404:
                    return NotFound(new ErrorResponse
                    {
                        Errors = response.Errors
                    });
                case 500:
                    return StatusCode(500, new ErrorResponse
                    {
                        Errors = response.Errors
                    });
                default:
                    return StatusCode(500, new ErrorResponse
                    {
                        Errors = response.Errors
                    });
            }
        }
    }
}