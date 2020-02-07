using AgroBarn.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AgroBarn.Domain.Repositories.V1
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByToken(Guid refreshToken);

        Task<RefreshToken> AddAsync(RefreshToken newRefreshToken);

        Task<bool> UpdateAsync(RefreshToken refreshToken);
    }
}
