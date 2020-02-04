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
    public class MessageController : ControllerBase
    {
        private readonly IAgroBarnSupervisor _agroBarnSupervisor;
        private readonly IMapper _mapper;

        public MessageController(
            IMapper mapper,
            IAgroBarnSupervisor agroBarnSupervisor
        )
        {
            _mapper = mapper;
            _agroBarnSupervisor = agroBarnSupervisor;
        }

        /// <summary>
        /// Obtiene la lista de todos los mensajes registrados
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Messages.GetAll)]
        public async Task<ActionResult<List<MessageResponse>>> GetAll()
        {
            List<MessageResult> result = await _agroBarnSupervisor.GetAllMessageAsync();
            List<MessageResponse> response = _mapper.Map<List<MessageResponse>>(result);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un mensaje a través de su ID 
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Messages.GetById)]
        public async Task<ActionResult<MessageResponse>> GetById([FromRoute] int messageId)
        {
            MessageResult result = await _agroBarnSupervisor.GetMessageByIdAsync(messageId);
            if (!result.Success)
                return ResponseErrorCode(result);

            MessageResponse message = _mapper.Map<MessageResponse>(result);
            return Ok(message);
        }

        /// <summary>
        /// Obtiene un mensaje a través del código
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Messages.GetByCode)]
        public async Task<ActionResult<MessageResponse>> GetByCode([FromRoute] string messageCode)
        {
            MessageResult result = await _agroBarnSupervisor.GetMessageByCodeAsync(messageCode);
            if (!result.Success)
                return ResponseErrorCode(result);

            MessageResponse message = _mapper.Map<MessageResponse>(result);
            return Ok(message);
        }

        /// <summary>
        /// Guarda un nuevo mensaje
        /// </summary>
        /// <param name="newMessage"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Messages.Create)]
        public async Task<ActionResult<MessageResponse>> Post([FromBody] MessageRequest newMessage)
        {
            int userId = 1;
            MessageResult result = await _agroBarnSupervisor.AddMessageAsync(newMessage, userId);

            if (!result.Success)
                return ResponseErrorCode(result);

            MessageResponse message = _mapper.Map<MessageResponse>(result);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Messages.GetById.Replace("{messageId}", message.Id.ToString());

            return Created(locationUri, message);
        }

        /// <summary>
        /// Actualiza los datos del mensaje, identificando el registro a través de su ID
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPatch(ApiRoutes.Messages.Update)]
        public async Task<ActionResult<MessageResponse>> Update([FromRoute] int messageId, [FromBody] MessageRequest message)
        {
            int userId = 1;
            MessageResult result = await _agroBarnSupervisor.UpdateMessageAsync(message, messageId, userId);

            if (!result.Success)
                return ResponseErrorCode(result);

            MessageResponse response = _mapper.Map<MessageResponse>(result);
            return Ok(response);
        }

        private ActionResult ResponseErrorCode(MessageResult response)
        {
            switch (response.CodeError)
            {
                case 400:
                    return BadRequest(new ErrorsResponse
                    {
                        Errors = response.Errors
                    });
                case 404:
                    return NotFound(new ErrorsResponse
                    {
                        Errors = response.Errors
                    });
                case 409:
                    return Conflict(new ErrorsResponse
                    {
                        Errors = response.Errors
                    });
                case 500:
                    return StatusCode(500, new ErrorsResponse
                    {
                        Errors = response.Errors
                    });
                default:
                    return StatusCode(500, new ErrorsResponse
                    {
                        Errors = response.Errors
                    });
            }
        }
    }
}