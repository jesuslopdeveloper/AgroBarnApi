using AgroBarn.Domain.Entities;
using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Result;
using AgroBarn.Domain.ApiModels.V1.Response;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroBarn.Domain.Supervisor.V1
{
    public partial class AgroBarnSupervisor
    {
        public async Task<List<MessageResult>> GetAllMessageAsync()
        {
            List<MessageDto> messagesDto = await _messageRepository.GetAllAsync();
            return messagesDto.Count > 0 ? _mapper.Map<List<MessageResult>>(messagesDto) : new List<MessageResult>();
        }

        public async Task<MessageResult> GetMessageByCodeAsync(string code)
        {
            MessageDto messageDto = await _messageRepository.GetByCodeAsync(code);
            return messageDto != null ? MessageResponseOK(messageDto) : await MessageResponseNotFound();
        }

        public async Task<MessageResult> GetMessageByIdAsync(int messageId)
        {
            MessageDto messageDto = await _messageRepository.GetByIdAsync(messageId);
            return messageDto != null ? MessageResponseOK(messageDto) : await MessageResponseNotFound();
        }

        public async Task<MessageResult> AddMessageAsync(MessageRequest newMessage, int userId)
        {
            try
            {
                MessageDto menssageExist = await _messageRepository.GetByCodeAsync(newMessage.Code);

                if (menssageExist != null)
                    return await MessageResponseDuplicate();

                MessageDto messageDto = _mapper.Map<MessageDto>(newMessage);
                messageDto.Status = 1;
                messageDto.UserCreate = userId;
                messageDto.DateCreate = DateTime.Now;

                messageDto = await _messageRepository.AddAsync(messageDto);
                return MessageResponseOK(messageDto);
            }
            catch (Exception)
            {
                //TODO Log Error
                return await MessageResponseInternalError();
            }
        }

        public async Task<MessageResult> UpdateMessageAsync(MessageRequest message, int messageId, int userId)
        {
            try
            {
                MessageDto messageDto = await _messageRepository.GetByIdAsync(messageId);
                if (messageDto == null)
                    return await MessageResponseNotFound();

                messageDto.Module = message.Module;
                messageDto.Code = message.Code;
                messageDto.Description = message.Description;
                messageDto.UserModify = userId;
                messageDto.DateModify = DateTime.Now;

                await _messageRepository.UpdateAsync(messageDto);
                return MessageResponseOK(messageDto);
            }
            catch (Exception)
            {
                //TODO Log Error
                return await MessageResponseInternalError();
            }
        }

        private MessageResult MessageResponseOK(MessageDto message)
        {
            MessageResult response = _mapper.Map<MessageResult>(message);
            response.Success = true;
            return response;
        }

        private async Task<MessageResult> MessageResponseNotFound()
        {
            MessageDto message = await _messageRepository.GetByCodeAsync("not-found");
            return new MessageResult
            {
                Success = false,
                CodeError = 404,
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

        private async Task<MessageResult> MessageResponseDuplicate()
        {
            MessageDto message = await _messageRepository.GetByCodeAsync("duplicate");
            return new MessageResult
            {
                Success = false,
                CodeError = 409,
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

        private async Task<MessageResult> MessageResponseInternalError()
        {
            MessageDto message = await _messageRepository.GetByCodeAsync("internal-server-error");
            return new MessageResult
            {
                Success = false,
                CodeError = 500,
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
