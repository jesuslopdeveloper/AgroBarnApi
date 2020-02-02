using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Result;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroBarn.Domain.Supervisor.V1
{
    public interface IAgroBarnSupervisor
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //Catalogs
        //Methods Entity Label
        Task<List<BreedResult>> GetAllBreedAsync();

        Task<BreedResult> GetBreedByIdAsync(int breedId);

        Task<BreedResult> GetBreedByNameAsync(string name);

        Task<BreedResult> AddBreedAsync(BreedRequest newBreed, int userId);

        Task<BreedResult> UpdateBreedAsync(BreedRequest breed, int breedId, int userId);

        Task<BreedResult> LowBreedAsync(int breedId, int userId);
    }
}
