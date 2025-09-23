using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ProductManagement.DTOs;
namespace ProductManagement.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOrderConfirmationEmail(OrderDTO order, string confirmationLink)
        {
            var emailSettings = _config.GetSection("EmailSettings");
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailSettings["SenderEmail"]));
            email.To.Add(MailboxAddress.Parse(order.CustomerEmail));
            email.Subject = $"Xác nhận đơn hàng #{order.OrderId}";

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $@"
                <h3>Cảm ơn bạn đã đặt hàng!</h3>
                <p>Thông tin đơn hàng:</p>
                <ul>
                    <li>Tên khách hàng: {order.CustomerName}</li>
                    <li>Email: {order.CustomerEmail}</li>
                    <li>Số điện thoại: {order.CustomerPhone}</li>
                    <li>Địa chỉ: {order.ShippingAddress}</li>
                    <li>Tổng tiền: {order.TotalPrice:N0} VNĐ</li>
                    <li>Ngày đặt hàng: {order.CreatedAt:dd/MM/yyyy}</li>
                    
                </ul>
                <p>Vui lòng xác nhận đơn hàng bằng cách nhấn vào link sau:</p>
                <a href='{confirmationLink}'>Xác nhận đơn hàng</a>
            "
            };

            using var smtp = new SmtpClient();
            var portString = emailSettings["Port"];
            if (!int.TryParse(portString, out var port))
            {
                throw new InvalidOperationException("EmailSettings:Port is not configured or not a valid integer.");
            }

            var senderEmail = emailSettings["SenderEmail"] ?? throw new InvalidOperationException("EmailSettings:SenderEmail is not configured.");
            var password = emailSettings["Password"] ?? throw new InvalidOperationException("EmailSettings:Password is not configured.");

            await smtp.ConnectAsync(emailSettings["SmtpServer"], port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(senderEmail, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
