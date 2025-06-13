using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.data;
using OrderApi.model;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _db;
        public OrderController(OrderDbContext context)
        {
            this._db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _db.Orders.ToListAsync();
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order) 
        {
            order.OrderDate = DateTime.Now;
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return Ok(order);
        }
    }

}
