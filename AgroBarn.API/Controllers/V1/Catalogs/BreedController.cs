using AgroBarn.Domain.Supervisor.V1;
using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Response;
using AgroBarn.Domain.ApiModels.V1.Result;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroBarn.API.Controllers.V1.Catalogs
{
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly IAgroBarnSupervisor _agroBarnSupervisor;

        public BreedController(
            IAgroBarnSupervisor agroBarnSupervisor
        )
        {
            _agroBarnSupervisor = agroBarnSupervisor;
        }

        [HttpGet("api/v1/breeds")]
        public async Task<ActionResult<List<BreedResponse>>> GetAll()
        {
            List<BreedResult> result = await _agroBarnSupervisor.GetAllBreedAsync();
            List<BreedResponse> breeds = new List<BreedResponse>();
            foreach (var item in result)
            {
                breeds.Add(new BreedResponse
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            return Ok(breeds);
        }

        [HttpGet("api/v1/breeds/{breedId}/id")]
        public async Task<ActionResult<BreedResponse>> GetById([FromRoute] int breedId)
        {
            BreedResult result = await _agroBarnSupervisor.GetBreedByIdAsync(breedId);
            if (!result.Success)
            {
                return NotFound();
            }

            BreedResponse breed = new BreedResponse();
            breed.Id = result.Id;
            breed.Name = result.Name;
            return Ok(breed);
        }

        [HttpGet("api/v1/breeds/{breedName}/name")]
        public async Task<ActionResult<BreedResponse>> GetByName([FromRoute] string breedName)
        {
            BreedResult result = await _agroBarnSupervisor.GetBreedByNameAsync(breedName);
            if (!result.Success)
            {
                return NotFound();
            }

            BreedResponse breed = new BreedResponse();
            breed.Id = result.Id;
            breed.Name = result.Name;
            return Ok(breed);
        }

        [HttpPost("api/v1/breeds")]
        public async Task<ActionResult<BreedResponse>> Post([FromBody]BreedRequest newBreed)
        {
            if (newBreed == null)
                return BadRequest();

            int userId = 1;
            BreedResult result = await _agroBarnSupervisor.AddBreedAsync(newBreed, userId);

            if (!result.Success)
                return BadRequest();

            BreedResponse breed = new BreedResponse();
            breed.Id = result.Id;
            breed.Name = result.Name;

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/api/v1/breeds/{breedId}/id".Replace("{breedId}", breed.Id.ToString());

            return Created(locationUri, breed);
        }

        [HttpPatch("api/v1/breeds/{breedId}")]
        public async Task<ActionResult<BreedResponse>> Update([FromRoute] int breedId, [FromBody]BreedRequest breed)
        {
            if (breed == null)
                return BadRequest();

            int userId = 1;
            BreedResult result = await _agroBarnSupervisor.UpdateBreedAsync(breed, breedId, userId);

            if (!result.Success)
                return BadRequest();

            BreedResponse response = new BreedResponse();
            response.Id = result.Id;
            response.Name = result.Name;

            return Ok(response);
        }

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