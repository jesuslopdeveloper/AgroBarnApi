using AgroBarn.Domain.Entities;
using AgroBarn.Domain.Repositories.V1;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace AgroBarn.Data.Repositories.V1
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AgroBarnContext _context;

        public RefreshTokenRepository(AgroBarnContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetByToken(Guid refreshToken)
        {
            return await _context.RefreshToken.SingleOrDefaultAsync(x => x.Token == refreshToken);
        }

        public async Task<RefreshToken> AddAsync(RefreshToken newRefreshToken)
        {
            await _context.RefreshToken.AddAsync(newRefreshToken);
            await _context.SaveChangesAsync();
            return newRefreshToken;
        }

        public async Task<bool> UpdateAsync(RefreshToken refreshToken)
        {
            _context.RefreshToken.Update(refreshToken);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
