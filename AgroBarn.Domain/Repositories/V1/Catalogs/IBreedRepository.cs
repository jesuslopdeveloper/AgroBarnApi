using AgroBarn.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroBarn.Domain.Repositories.V1
{
    public interface IBreedRepository
    {
        Task<List<BreedDto>> GetAllAsync();

        Task<BreedDto> GetByIdAsync(int id);

        Task<BreedDto> GetByNameAsync(string name);

        Task<BreedDto> AddAsync(BreedDto newBreed);

        Task<bool> UpdateAsync(BreedDto breed);
    }
}
