using AgroBarn.Domain.Supervisor.V1;
using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Response;
using AgroBarn.Domain.ApiModels.V1.Result;
using AgroBarn.API.Contracts.V1;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace AgroBarn.API.Controllers.V1.Catalogs
{
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly IAgroBarnSupervisor _agroBarnSupervisor;
        private readonly IMapper _mapper;

        public BreedController(
            IMapper mapper,
            IAgroBarnSupervisor agroBarnSupervisor
        )
        {
            _mapper = mapper;
            _agroBarnSupervisor = agroBarnSupervisor;
        }

        /// <summary>
        /// Obtiene la lista de todas las razas registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Breeds.GetAll)]
        public async Task<ActionResult<List<BreedResponse>>> GetAll()
        {
            List<BreedResult> result = await _agroBarnSupervisor.GetAllBreedAsync();
            List<BreedResponse> response = _mapper.Map<List<BreedResponse>>(result);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una raza a través de su ID
        /// </summary>
        /// <param name="breedId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Breeds.GetById)]
        public async Task<ActionResult<BreedResponse>> GetById([FromRoute] int breedId)
        {
            BreedResult result = await _agroBarnSupervisor.GetBreedByIdAsync(breedId);
            if (!result.Success)
                return ResponseErrorCode(result);

            BreedResponse breed = _mapper.Map<BreedResponse>(result);
            return Ok(breed);
        }

        /// <summary>
        /// Obtiene solo una raza a través de su Nombre
        /// </summary>
        /// <param name="breedName"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Breeds.GetByName)]
        public async Task<ActionResult<BreedResponse>> GetByName([FromRoute] string breedName)
        {
            BreedResult result = await _agroBarnSupervisor.GetBreedByNameAsync(breedName);
            if (!result.Success)
                return ResponseErrorCode(result);

            BreedResponse breed = _mapper.Map<BreedResponse>(result);
            return Ok(breed);
        }

        /// <summary>
        /// Guarda una nueva raza
        /// </summary>
        /// <param name="newBreed"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Breeds.Create)]
        public async Task<ActionResult<BreedResponse>> Post([FromBody] BreedRequest newBreed)
        {
            int userId = 1;
            BreedResult result = await _agroBarnSupervisor.AddBreedAsync(newBreed, userId);

            if (!result.Success)
                return ResponseErrorCode(result);

            BreedResponse breed = _mapper.Map<BreedResponse>(result);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Breeds.GetById.Replace("{breedId}", breed.Id.ToString());

            return Created(locationUri, breed);
        }

        /// <summary>
        /// Actualiza el nombre de una raza, identificando el registro a través de su ID
        /// </summary>
        /// <param name="breedId"></param>
        /// <param name="breed"></param>
        /// <returns></returns>
        [HttpPatch(ApiRoutes.Breeds.Update)]
        public async Task<ActionResult<BreedResponse>> Update([FromRoute] int breedId, [FromBody] BreedRequest breed)
        {
            int userId = 1;
            BreedResult result = await _agroBarnSupervisor.UpdateBreedAsync(breed, breedId, userId);

            if (!result.Success)
                return ResponseErrorCode(result);

            BreedResponse response = _mapper.Map<BreedResponse>(result);
            return Ok(response);
        }

        /// <summary>
        /// Cambia el estatus del registro a Baja
        /// </summary>
        /// <param name="breedId"></param>
        /// <returns></returns>
        [HttpPatch(ApiRoutes.Breeds.Low)]
        public async Task<ActionResult<BreedResponse>> Low([FromRoute] int breedId)
        {
            int userId = 1;
            BreedResult result = await _agroBarnSupervisor.LowBreedAsync(breedId, userId);

            if (!result.Success)
                return ResponseErrorCode(result);

            return NoContent();
        }

        private ActionResult ResponseErrorCode(BreedResult response)
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
                case 409:
                    return Conflict(new ErrorResponse
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