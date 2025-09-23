using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IChatService
    {
        Task<ChatRoomDTO> GetOrCreateRoomAsync(int customerId, int adminId);
        Task<List<ChatRoomDTO>> GetChatRoomsForAdminAsync(int adminId);
        Task<List<ChatMessageDTO>> GetMessagesAsync(int chatRoomId);
        Task<ChatMessageDTO> AddMessageAsync(ChatMessageDTO messageDto);
        Task MarkMessageAsReadAsync(int messageId);
    }
}
