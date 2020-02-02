using AgroBarn.Domain.Entities;
using AgroBarn.Domain.Repositories.V1;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgroBarn.Data.Repositories.V1
{
    public class BreedRepository : IBreedRepository
    {
        private readonly AgroBarnContext _context;

        public BreedRepository(AgroBarnContext context)
        {
            _context = context;
        }

        private async Task<bool> BreedExists(int id) =>
            await _context.BreedDto.AnyAsync(x => x.Id == id);

        public async Task<List<BreedDto>> GetAllAsync()
        {
            return await _context.BreedDto.ToListAsync();
        }

        public async Task<BreedDto> GetByIdAsync(int id)
        {
            return await _context.BreedDto.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BreedDto> GetByNameAsync(string name)
        {
            return await _context.BreedDto.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<BreedDto> AddAsync(BreedDto newBreed)
        {
            await _context.BreedDto.AddAsync(newBreed);
            await _context.SaveChangesAsync();
            return newBreed;
        }

        public async Task<bool> UpdateAsync(BreedDto breed)
        {
            if (!await BreedExists(breed.Id))
                return false;

            _context.BreedDto.Update(breed);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
