using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IGeminiService
    {
        Task<string> GetResponseAsync(string prompt);
        Task<string> GetProductAnswerAsync(ProductDTO product, string userQuestion);
        Task<ExtractedIntentInfo> ExtractIntentAsync(string userQuestion);
        Task<string> GetOrderAnswerAsync(OrderResponseDTO? orderDTO, string message);
    }

    
}
