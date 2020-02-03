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
            List<BreedResult> breedsResult = new List<BreedResult>();
            List<BreedDto> breedsDto = await _breedRepository.GetAllAsync();
            foreach (var item in breedsDto)
            {
                //TODO
                //Conversion

                breedsResult.Add(new BreedResult
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            return breedsResult;
        }

        public async Task<BreedResult> GetBreedByIdAsync(int breedId)
        {
            BreedDto breed = await _breedRepository.GetByIdAsync(breedId);
            if (breed != null)
            {
                //TODO
                //Conversion

                return new BreedResult
                {
                    Id = breed.Id,
                    Name = breed.Name,
                    Success = true
                };
            }
            else
            {
                //TODO
                //Devolver mensaje
                return new BreedResult
                {
                    Success = false
                };
            }
        }

        public async Task<BreedResult> GetBreedByNameAsync(string name)
        {
            BreedDto breed = await _breedRepository.GetByNameAsync(name);
            if (breed != null)
            {
                //TODO
                //Conversion

                return new BreedResult
                {
                    Id = breed.Id,
                    Name = breed.Name,
                    Success = true
                };
            }
            else
            {
                //TODO
                //Devolver mensaje
                return new BreedResult
                {
                    Success = false
                };
            }
        }

        public async Task<BreedResult> AddBreedAsync(BreedRequest newBreed, int userId)
        {
            try
            {
                BreedDto breedExist = await _breedRepository.GetByNameAsync(newBreed.Name);
                if (breedExist == null)
                {
                    //TODO
                    //Conversion
                    BreedDto breedDto = new BreedDto();
                    breedDto.Name = newBreed.Name;
                    breedDto.Status = 1;
                    breedDto.UserCreate = userId;
                    breedDto.DateCreate = DateTime.Now;
                    breedDto = await _breedRepository.AddAsync(breedDto);

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

        public async Task<BreedResult> UpdateBreedAsync(BreedRequest breed, int breedId,  int userId)
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
    }
}
