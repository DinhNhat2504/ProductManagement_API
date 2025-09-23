using AutoMapper;
using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repo;
        private readonly IMapper _mapper;
        public ChatService(IChatRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ChatRoomDTO>> GetChatRoomsForAdminAsync(int adminId)
        {
            var rooms = await _repo.GetChatRoomsForAdminAsync(adminId);
            return _mapper.Map<List<ChatRoomDTO>>(rooms);
        }

        public async Task<List<ChatMessageDTO>> GetMessagesAsync(int chatRoomId)
        {
            var messages = await _repo.GetMessagesAsync(chatRoomId);
            return _mapper.Map<List<ChatMessageDTO>>(messages);
        }

        public async Task<ChatMessageDTO> AddMessageAsync(ChatMessageDTO messageDto)
        {
            var message = new ChatMessage
            {
                ChatRoomId = messageDto.ChatRoomId,
                SenderId = messageDto.SenderId,
                Content = messageDto.Content,
                SentAt = DateTime.UtcNow
            };
            var result = await _repo.AddMessageAsync(message);
            return _mapper.Map<ChatMessageDTO>(result);
        }

        public async Task MarkMessageAsReadAsync(int messageId)
        {
            await _repo.MarkMessageAsReadAsync(messageId);
        }

        public async Task<ChatRoomDTO> GetOrCreateRoomAsync(int customerId, int adminId)
        {
            var room = await _repo.GetOrCreateRoomAsync(customerId, adminId);
            return _mapper.Map<ChatRoomDTO>(room);
        }
    }
}
