using System.Collections.Generic;

namespace ProductManagement.DTOs
{
    public class BrandPagedResultDTO
    {
        public IEnumerable<BrandDTO> Items { get; set; } = new List<BrandDTO>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
