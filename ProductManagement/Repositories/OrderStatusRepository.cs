using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly AppDbContext _context;
        public OrderStatusRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderStatus>> GetAllStatusAsync()
        {
            return await _context.OrderStatuses.ToListAsync();
        }
        public async Task<OrderStatus?> GetStatusByIdAsync(int statusId)
        {
            return await _context.OrderStatuses.FindAsync(statusId);
        }
    }
}
