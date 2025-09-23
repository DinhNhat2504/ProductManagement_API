using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;
        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatRoom>> GetChatRoomsForAdminAsync(int adminId)
        {
            return await _context.Chats
                .Include(r => r.User)
                .Where(r => r.AdminId == adminId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(int chatRoomId)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ChatMessage> AddMessageAsync(ChatMessage message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task MarkMessageAsReadAsync(int messageId)
        {
            var msg = await _context.Messages.FindAsync(messageId);
            if (msg != null)
            {
                // Directly set the property instead of using reflection
                msg.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ChatRoom> GetOrCreateRoomAsync(int customerId, int adminId)
        {
            var existingRoom = await _context.Chats
                .Where(r => r.User!.RoleId != 1 && r.Admin!.RoleId == 1)
                .FirstOrDefaultAsync(r => r.UserId == customerId && r.AdminId == adminId);

            if (existingRoom != null)
            {
                return existingRoom;
            }

            var newRoom = new ChatRoom
            {
                UserId = customerId,
                AdminId = adminId
            };

            _context.Chats.Add(newRoom);
            await _context.SaveChangesAsync();

            return newRoom;
        }
    }
}
