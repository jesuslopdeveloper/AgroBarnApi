using AgroBarn.Domain.Supervisor.V1;
using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Response;
using AgroBarn.Domain.ApiModels.V1.Result;

using System;
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
        [HttpGet("api/v1/breeds")]
        public async Task<ActionResult<List<BreedResponse>>> GetAll()
        {
            List<BreedResult> result = await _agroBarnSupervisor.GetAllBreedAsync();
            List<BreedResponse> response = _mapper.Map<List<BreedResponse>>(result);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene solo una raza a través de su ID
        /// </summary>
        /// <param name="breedId"></param>
        /// <returns></returns>
        [HttpGet("api/v1/breeds/{breedId}/id")]
        public async Task<ActionResult<BreedResponse>> GetById([FromRoute] int breedId)
        {
            BreedResult result = await _agroBarnSupervisor.GetBreedByIdAsync(breedId);
            if (!result.Success)
            {
                return NotFound();
            }

            BreedResponse breed = _mapper.Map<BreedResponse>(result);
            return Ok(breed);
        }

        /// <summary>
        /// Obtiene solo una raza a través de su Nombre
        /// </summary>
        /// <param name="breedName"></param>
        /// <returns></returns>
        [HttpGet("api/v1/breeds/{breedName}/name")]
        public async Task<ActionResult<BreedResponse>> GetByName([FromRoute] string breedName)
        {
            BreedResult result = await _agroBarnSupervisor.GetBreedByNameAsync(breedName);
            if (!result.Success)
            {
                return NotFound();
            }

            BreedResponse breed = _mapper.Map<BreedResponse>(result);
            return Ok(breed);
        }

        /// <summary>
        /// Guarda una nueva raza
        /// </summary>
        /// <param name="newBreed"></param>
        /// <returns></returns>
        [HttpPost("api/v1/breeds")]
        public async Task<ActionResult<BreedResponse>> Post([FromBody]BreedRequest newBreed)
        {
            if (newBreed == null)
                return BadRequest();

            int userId = 1;
            BreedResult result = await _agroBarnSupervisor.AddBreedAsync(newBreed, userId);

            if (!result.Success)
                return BadRequest();

            BreedResponse breed = _mapper.Map<BreedResponse>(result);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/api/v1/breeds/{breedId}/id".Replace("{breedId}", breed.Id.ToString());

            return Created(locationUri, breed);
        }

        /// <summary>
        /// Actualiza el nombre de una raza, identificando el registro a través de su ID
        /// </summary>
        /// <param name="breedId"></param>
        /// <param name="breed"></param>
        /// <returns></returns>
        [HttpPatch("api/v1/breeds/{breedId}")]
        public async Task<ActionResult<BreedResponse>> Update([FromRoute] int breedId, [FromBody]BreedRequest breed)
        {
            if (breed == null)
                return BadRequest();

            int userId = 1;
            BreedResult result = await _agroBarnSupervisor.UpdateBreedAsync(breed, breedId, userId);

            if (!result.Success)
                return BadRequest();

            BreedResponse response = _mapper.Map<BreedResponse>(result);
            return Ok(response);
        }

        /// <summary>
        /// Cambia el estatus del registro a Baja
        /// </summary>
        /// <param name="breedId"></param>
        /// <returns></returns>
        [HttpPatch("api/v1/breeds/{breedId}/low")]
        public async Task<ActionResult<BreedResponse>> Low([FromRoute] int breedId)
        {
            int userId = 1;
            BreedResult result = await _agroBarnSupervisor.LowBreedAsync(breedId, userId);

            if (!result.Success)
                return BadRequest();

            return NoContent();
        }
    }
}