using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public interface IChatRepository
    {
        Task<ChatRoom> GetOrCreateRoomAsync(int customerId, int adminId);
        Task<List<ChatRoom>> GetChatRoomsForAdminAsync(int adminId);
        Task<List<ChatMessage>> GetMessagesAsync(int chatRoomId);
        Task<ChatMessage> AddMessageAsync(ChatMessage message);
        Task MarkMessageAsReadAsync(int messageId);
    }
}
