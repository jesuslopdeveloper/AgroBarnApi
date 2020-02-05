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
        public async Task<List<BreedResult>> GetAllBreedAsync()
        {
            List<BreedDto> breedsDto = await _breedRepository.GetAllAsync();
            return breedsDto.Count > 0 ? _mapper.Map<List<BreedResult>>(breedsDto) : new List<BreedResult>();
        }

        public async Task<BreedResult> GetBreedByIdAsync(int breedId)
        {
            BreedDto breed = await _breedRepository.GetByIdAsync(breedId);
            return breed != null ? BreedResponseOK(breed) : await BreedResponseNotFound();
        }

        public async Task<BreedResult> GetBreedByNameAsync(string name)
        {
            BreedDto breed = await _breedRepository.GetByNameAsync(name);
            return breed != null ? BreedResponseOK(breed) : await BreedResponseNotFound();
        }

        public async Task<BreedResult> AddBreedAsync(BreedRequest newBreed, int userId)
        {
            try
            {
                BreedDto breedExist = await _breedRepository.GetByNameAsync(newBreed.Name);

                if (breedExist != null)
                    return await BreedResponseDuplicate();

                BreedDto breedDto = _mapper.Map<BreedDto>(newBreed);
                breedDto.Status = 1;
                breedDto.UserCreate = userId;
                breedDto.DateCreate = DateTime.Now;

                breedDto = await _breedRepository.AddAsync(breedDto);
                return BreedResponseOK(breedDto);
            }
            catch (Exception)
            {
                //TODO Log Error
                return await BreedResponseInternalError();
            }
        }

        public async Task<BreedResult> UpdateBreedAsync(BreedRequest breed, int breedId, int userId)
        {
            try
            {
                BreedDto breedDto = await _breedRepository.GetByIdAsync(breedId);
                if (breedDto == null)
                    return await BreedResponseNotFound();

                breedDto.Name = breed.Name;
                breedDto.Status = 1;
                breedDto.UserModify = userId;
                breedDto.DateModify = DateTime.Now;

                await _breedRepository.UpdateAsync(breedDto);
                return BreedResponseOK(breedDto);
            }
            catch (Exception)
            {
                //TODO Log Error
                return await BreedResponseInternalError();
            }
        }

        public async Task<BreedResult> LowBreedAsync(int breedId, int userId)
        {
            try
            {
                BreedDto breedDto = await _breedRepository.GetByIdAsync(breedId);

                if (breedDto == null)
                    return await BreedResponseNotFound();

                breedDto.Status = 0;
                breedDto.DateModify = DateTime.Now;
                breedDto.UserModify = userId;

                await _breedRepository.UpdateAsync(breedDto);
                return BreedResponseOK(breedDto);
            }
            catch (Exception)
            {
                //TODO Log Error
                return await BreedResponseInternalError();
            }
        }

        private BreedResult BreedResponseOK(BreedDto breed)
        {
            BreedResult response = _mapper.Map<BreedResult>(breed);
            response.Success = true;
            return response;
        }

        private async Task<BreedResult> BreedResponseNotFound()
        {
            MessageDto message = await _messageRepository.GetByCodeAsync("not-found");
            return new BreedResult
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

        private async Task<BreedResult> BreedResponseDuplicate()
        {
            MessageDto message = await _messageRepository.GetByCodeAsync("duplicate");
            return new BreedResult
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

        private async Task<BreedResult> BreedResponseInternalError()
        {
            MessageDto message = await _messageRepository.GetByCodeAsync("internal-server-error");
            return new BreedResult
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
