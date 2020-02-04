using AgroBarn.Domain.Entities;
using AgroBarn.Domain.Repositories.V1;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgroBarn.Data.Repositories.V1
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AgroBarnContext _context;

        public MessageRepository(AgroBarnContext context)
        {
            _context = context;
        }

        private async Task<bool> MessageExist(int id) =>
            await _context.MessageDto.AnyAsync(x => x.Id == id);

        public async Task<List<MessageDto>> GetAllAsync()
        {
            return await _context.MessageDto.ToListAsync();
        }

        public async Task<MessageDto> GetByCodeAsync(string code)
        {
            return await _context.MessageDto.SingleOrDefaultAsync(x => x.Code == code);
        }

        public async Task<MessageDto> GetByIdAsync(int id)
        {
            return await _context.MessageDto.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MessageDto> AddAsync(MessageDto newMessage)
        {
            await _context.MessageDto.AddAsync(newMessage);
            await _context.SaveChangesAsync();
            return newMessage;
        }

        public async Task<bool> UpdateAsync(MessageDto message)
        {
            if (!await MessageExist(message.Id))
                return false;

            _context.MessageDto.Update(message);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
