using AgroBarn.Domain.Entities;

using System.Threading.Tasks;

namespace AgroBarn.Domain.Repositories.V1
{
    public interface IPeopleRepository
    {
        Task<PeopleDto> GetByIdAsync(int id);

        Task<PeopleDto> GetByUserIdAsync(int userId);

        Task<bool> AddAsync(PeopleDto newPeople);

        Task<bool> UpdateAsync(PeopleDto people);

        Task<bool> LowAsync(int id);
    }
}
