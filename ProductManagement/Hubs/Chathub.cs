using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ProductManagement.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int chatRoomId, int senderId, string message)
        {
            
        }

        public async Task JoinRoom(int chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }
        // Đánh dấu tin nhắn đã đọc
        public async Task MarkAsRead(int messageId)
        {
            await Clients.All.SendAsync("MessageRead", messageId);
        }
    }
}
