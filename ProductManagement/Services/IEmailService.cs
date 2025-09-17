using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IEmailService
    {
        Task SendOrderConfirmationEmail(OrderDTO order, string confirmationLink);
    }
}
