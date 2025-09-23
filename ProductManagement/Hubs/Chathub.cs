using Microsoft.AspNetCore.SignalR;
using ProductManagement.Services;
using ProductManagement.DTOs;
using System.Text.RegularExpressions;

namespace ProductManagement.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        // Gửi tin nhắn vào phòng (lưu vào DB rồi broadcast cho nhóm)
        public async Task SendMessage(int chatRoomId, int senderId, string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            // Basic sanitization: remove script tags
            message = Regex.Replace(message, "<script.*?>.*?</script>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            var dto = new ChatMessageDTO
            {
                ChatRoomId = chatRoomId,
                SenderId = senderId,
                Content = message,
                SentAt = DateTime.UtcNow
            };

            var saved = await _chatService.AddMessageAsync(dto);

            // Broadcast only to the chat room group
            await Clients.Group(chatRoomId.ToString()).SendAsync("ReceiveMessage", saved);
        }

        public async Task JoinRoom(int chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }

        // Đánh dấu tin nhắn đã đọc
        public async Task MarkAsRead(int messageId)
        {
            await _chatService.MarkMessageAsReadAsync(messageId);
            // Notify members of the room(s) that message was read. Client can handle the event.
            await Clients.All.SendAsync("MessageRead", messageId);
        }
    }
}
