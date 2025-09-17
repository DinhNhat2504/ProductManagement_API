using ProductManagement.DTOs;
using System.Text.Json;

namespace ProductManagement.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public GeminiService(IConfiguration configuration)
        {
            _apiKey = configuration["GeminiSettings:ApiKey"];
            _apiUrl = configuration["GeminiSettings:ApiUrl"];
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            using var client = new HttpClient();
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var requestUrl = $"{_apiUrl}{_apiKey}";
            var jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(requestUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Không thể lấy phản hồi từ Gemini API: " + e.Message);
            }
        }

        // Mở rộng: Truy vấn sản phẩm và truyền dữ liệu động vào prompt
        public async Task<string> GetProductAnswerAsync(ProductDTO product, string userQuestion)
        {
            // Tạo prompt động dựa trên dữ liệu sản phẩm
            var prompt = $"Khách hàng hỏi: \"{userQuestion}\". Dưới đây là thông tin sản phẩm:\n" +
                         $"- Tên: {product.Name}\n" +
                         $"- Giá: {product.Price} VNĐ\n" +
                         $"- Mô tả: {product.Description}\n" +
                         //$"- Tồn kho: {product.}\n" +
                         $"Hãy trả lời khách bằng tiếng Việt, thân thiện và dễ hiểu.";

            return await GetResponseAsync(prompt);
        }
        public async Task<string> GetOrderAnswerAsync(OrderResponseDTO order, string userQuestion)
        {
            // Tạo prompt động dựa trên dữ liệu đơn hàng
            var prompt = $"Khách hàng hỏi: \"{userQuestion}\". Dưới đây là thông tin đơn hàng:\n" +
                         $"- Mã đơn hàng: {order.OrderId}\n" +
                            $"- Tên khách hàng: {order.CustomerName}\n" +
                            $"- Email khách hàng: {order.CustomerEmail}\n" +
                         $"- Danh sách sản phẩm: {string.Join(", ", order.OrderItems.Select(item => item.ProductName))}\n" +
                         $"- Số lượng: {string.Join(", ", order.OrderItems.Select(item => item.Quantity))}\n" +
                         $"- Trạng thái: {order.OrderStatus}\n" +
                         $"- Phương thức thanh toán: {order.PaymentMethod}\n" +
                         $"- Địa chỉ giao hàng: {order.ShippingAddress}, {order.ShippingWard}, {order.ShippingDistrict}, {order.ShippingProvince}\n" +
                         $"- Ngày đặt hàng: {order.OrderDate}\n" +
                         $"- Tổng giá: {order.TotalPrice} VNĐ\n" +
                         $"Hãy trả lời khách bằng tiếng Việt, thân thiện và dễ hiểu.";

            return await GetResponseAsync(prompt);
        }

        public async Task<ExtractedIntentInfo> ExtractIntentAsync(string userQuestion)
        {
            var prompt = $"Phân tích câu hỏi sau và trả về một chuỗi JSON với các trường: " +
                 $"\"Intent\" (ý định: ListProducts, OrderInfo, CartInfo, ProductDetail, ...) và \"ProductName\" (nếu có). " +
                 $"Gợi ý : " +
                 $" - Trong câu hỏi mà có từ Sản phẩm , Điện thoại , Laptop , Tai nghe , Bàn phím và phía sau đó chắc chắc là tên của từ Sản phẩm , Điện thoại , Laptop , Tai nghe , Bàn phím, thì kết quả của \"Intent\":\"ProductDetail\",\"ProductName\":\"tên Sản phẩm , Điện thoại , Laptop , Tai nghe , Bàn phím\"}} " +
                 $"- Trong câu hỏi mà có từ Đơn hàng  thì kết quả của \"Intent\":\"OrderInfo\"" +
                 $"- Trong câu hỏi mà có từ Giỏ hàng thì kết quả của \"Intent\":\"CartInfo\"" +
                 $"- Trong câu hỏi mà có từ Những,Liệt kê,Danh sách...  thì kết quả của \"Intent\":\"ListProducts\"" +
                 $"Chỉ trả về JSON, không trả lời gì khác.\nCâu hỏi: \"{userQuestion}\"";

            var response = await GetResponseAsync(prompt);

            // Bước 1: Parse response để lấy phần text
            using var doc = JsonDocument.Parse(response);
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            // Bước 2: Lọc lấy JSON từ text (loại bỏ ```json và xuống dòng)
            var jsonMatch = System.Text.RegularExpressions.Regex.Match(text ?? "", @"\{[\s\S]*\}");
            if (jsonMatch.Success)
            {
                var json = jsonMatch.Value;
                // Bước 3: Deserialize sang ExtractedIntentInfo
                var intentInfo = JsonSerializer.Deserialize<ExtractedIntentInfo>(json);
                return intentInfo ?? new ExtractedIntentInfo();
            }
             return new ExtractedIntentInfo();
        }
    }
}
