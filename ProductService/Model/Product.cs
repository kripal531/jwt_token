using Microsoft.EntityFrameworkCore;

namespace ProductService.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        
        [Precision(18, 2)]
        public decimal productPrice { get; set; }
        
    }
}
