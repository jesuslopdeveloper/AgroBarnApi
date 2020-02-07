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
        //Identity
        //Methods Entity User and People
        Task<List<UserResult>> GetAllUserAsync();

        Task<UserResult> GetUserByIdAsync(int userId);

        //---------------------------------------------------------------------------------------------------------------------------------------------
        //Catalogs
        //Methods Entity Breed
        Task<List<BreedResult>> GetAllBreedAsync();

        Task<BreedResult> GetBreedByIdAsync(int breedId);

        Task<BreedResult> GetBreedByNameAsync(string name);

        Task<BreedResult> AddBreedAsync(BreedRequest newBreed, int userId);

        Task<BreedResult> UpdateBreedAsync(BreedRequest breed, int breedId, int userId);

        Task<BreedResult> LowBreedAsync(int breedId, int userId);

        //---------------------------------------------------------------------------------------------------------------------------------------------
        //Catalogs
        //Methods Entity Breed
        Task<List<MessageResult>> GetAllMessageAsync();

        Task<MessageResult> GetMessageByIdAsync(int messageId);

        Task<MessageResult> GetMessageByCodeAsync(string code);

        Task<MessageResult> AddMessageAsync(MessageRequest newMessage, int userId);

        Task<MessageResult> UpdateMessageAsync(MessageRequest message, int messageId, int userId);        
    }
}
