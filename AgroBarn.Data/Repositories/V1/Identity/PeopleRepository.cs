using AgroBarn.Domain.Entities;
using AgroBarn.Domain.Repositories.V1;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgroBarn.Data.Repositories.V1
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly AgroBarnContext _context;

        public PeopleRepository(AgroBarnContext context)
        {
            _context = context;
        }

        private async Task<bool> PeopleExist(int id) =>
            await _context.PeopleDto.AnyAsync(x => x.Id == id);

        public async Task<PeopleDto> GetByIdAsync(int id)
        {
            return await _context.PeopleDto.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PeopleDto> GetByUserIdAsync(int userId)
        {
            return await _context.PeopleDto.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<bool> AddAsync(PeopleDto newPeople)
        {
            await _context.PeopleDto.AddAsync(newPeople);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(PeopleDto people)
        {
            if (!await PeopleExist(people.Id))
                return false;

            _context.PeopleDto.Update(people);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LowAsync(int id)
        {
            PeopleDto people = await GetByIdAsync(id);

            if(people == null)
                return false;

            people.Status = 0;
            _context.PeopleDto.Update(people);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
