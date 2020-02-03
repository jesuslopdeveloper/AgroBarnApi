using AgroBarn.Domain.Entities;
using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Result;

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
            return breed != null ? BreedResponseOK(breed) : BreedResponseNotFound();
        }

        public async Task<BreedResult> GetBreedByNameAsync(string name)
        {
            BreedDto breed = await _breedRepository.GetByNameAsync(name);
            return breed != null ? BreedResponseOK(breed) : BreedResponseNotFound();
        }

        public async Task<BreedResult> AddBreedAsync(BreedRequest newBreed, int userId)
        {
            try
            {
                BreedDto breed = await _breedRepository.GetByNameAsync(newBreed.Name);

                if (breed != null)
                {
                    return BreedResponseConflict();
                }

                BreedDto breedDto = _mapper.Map<BreedDto>(newBreed);
                breedDto.Status = 1;
                breedDto.UserCreate = userId;
                breedDto.DateCreate = DateTime.Now;
                breedDto = await _breedRepository.AddAsync(breedDto);

                BreedResult response = _mapper.Map<BreedResult>(breedDto);
                response.Success = true;

                return response;
            }
            catch (Exception)
            {
                //TODO
                //Devolver mensaje de error
                //Guardar log error
                return BreedResponseInternalError();
            }
        }

        public async Task<BreedResult> UpdateBreedAsync(BreedRequest breed, int breedId, int userId)
        {
            try
            {
                BreedDto breedDto = await _breedRepository.GetByIdAsync(breedId);
                if (breedDto != null)
                {
                    //TODO
                    //Conversion
                    breedDto.Name = breed.Name;
                    breedDto.Status = 1;
                    breedDto.UserModify = userId;
                    breedDto.DateModify = DateTime.Now;
                    await _breedRepository.UpdateAsync(breedDto);

                    //TODO
                    //Conversion
                    BreedResult response = new BreedResult();
                    response.Id = breedDto.Id;
                    response.Name = breedDto.Name;
                    response.Success = true;

                    return response;
                }
                else
                {
                    //TODO
                    //Devolver mensaje de error
                    return new BreedResult
                    {
                        Success = false
                    };
                }
            }
            catch (Exception)
            {
                //TODO
                //Devolver mensaje de error
                //Guardar log error
                return new BreedResult
                {
                    Success = false
                };
            }
        }

        public async Task<BreedResult> LowBreedAsync(int breedId, int userId)
        {
            try
            {
                BreedDto breedDto = await _breedRepository.GetByIdAsync(breedId);
                if (breedDto != null)
                {
                    breedDto.Status = 0;
                    breedDto.DateModify = DateTime.Now;
                    breedDto.UserModify = userId;

                    await _breedRepository.UpdateAsync(breedDto);

                    return new BreedResult
                    {
                        Success = true
                    };
                }
                else
                {
                    //TODO
                    //Devolver mensaje de error
                    return new BreedResult
                    {
                        Success = false
                    };
                }
            }
            catch (Exception)
            {
                //TODO
                //Devolver mensaje de error
                //Guardar log error
                return new BreedResult
                {
                    Success = false
                };
            }
        }

        private BreedResult BreedResponseOK(BreedDto breed)
        {
            BreedResult response = _mapper.Map<BreedResult>(breed);
            response.Success = true;
            return response;
        }

        private BreedResult BreedResponseNotFound()
        {
            return new BreedResult
            {
                Success = false
            };

            //TODO
            //Manejo de mensajes
        }

        private BreedResult BreedResponseInternalError()
        {
            return new BreedResult
            {
                Success = false
            };

            //TODO
            //Manejo de mensajes
        }

        private BreedResult BreedResponseConflict()
        {
            return new BreedResult
            {
                Success = false
            };

            //TODO
            //Manejo de mensajes
        }
    }
}
