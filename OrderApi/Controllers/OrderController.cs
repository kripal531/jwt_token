using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.data;
using OrderApi.model;
using OrderApi.ProductService;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       // private readonly OrderDbContext _db;
       private readonly ProductServiceClient _productClient;
        private readonly OrderDbContext _db;
        public OrderController(ProductServiceClient productService, OrderDbContext db)
        {
            _productClient = productService;
            _db = db;
        }

        [HttpGet("place/{productId}/{qty}")]

        public async Task<IActionResult> PlacrOrder(int productId, int qty) 
        {
            var product = await _productClient.GetProductById(productId);
            if (product == null)
            {
                return NotFound("product not found");
            }
            var order= new Order
            {
               // Id= new Random().Next(1, 1000),
                ProductId = productId,
                ProductName= product.ProductName,
                Quantity = qty,
                OrderDate = DateTime.Now
                
            };
            var total = product.ProductPrice * qty;
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
               

            return Ok(order);
        }
       
    }

}
