using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProductManagement.DTOs;
using ProductManagement.Hubs;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatController(IChatService service, IHubContext<ChatHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
        }
        [HttpPost("get-or-create-room")]
        public async Task<IActionResult> GetOrCreateRoom([FromBody] GetOrCreateRoomDto dto)
        {
            // dto gồm: customerId, adminId
            var room = await _service.GetOrCreateRoomAsync(dto.CustomerId, dto.AdminId);
            return Ok(new { chatRoomId = room.ChatRoomId });
        }
        [HttpGet("rooms/{adminId}")]
        public async Task<IActionResult> GetChatRoomsForAdmin(int adminId)
        {
            var rooms = await _service.GetChatRoomsForAdminAsync(adminId);
            return Ok(rooms);
        }

        [HttpGet("messages/{chatRoomId}")]
        public async Task<IActionResult> GetMessages(int chatRoomId)
        {
            var messages = await _service.GetMessagesAsync(chatRoomId);
            return Ok(messages);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageDTO dto)
        {
            var result = await _service.AddMessageAsync(dto);
            // Phát tin nhắn real-time cho các client trong phòng chat
            await _hubContext.Clients.Group(dto.ChatRoomId.ToString())
                .SendAsync("ReceiveMessage", dto.SenderId, dto.Content, result.SentAt);

            return Ok(result);
        }

        [HttpPost("read/{messageId}")]
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            await _service.MarkMessageAsReadAsync(messageId);
            // Phát sự kiện tin nhắn đã đọc
            await _hubContext.Clients.All.SendAsync("MessageRead", messageId);
            return Ok();
        }
    }
}
