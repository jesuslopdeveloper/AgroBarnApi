using AgroBarn.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroBarn.Domain.Repositories.V1
{
    public interface IMessageRepository
    {
        Task<List<MessageDto>> GetAllAsync();

        Task<MessageDto> GetByIdAsync(int id);

        Task<MessageDto> GetByCodeAsync(string code);

        Task<MessageDto> AddAsync(MessageDto newMessage);

        Task<bool> UpdateAsync(MessageDto message);
    }
}
