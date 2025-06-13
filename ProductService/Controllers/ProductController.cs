using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Dbcontext;
using ProductService.Model;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;  
            
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var data = await _applicationDbContext.Products.ToListAsync();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            _applicationDbContext.Products.Add(product);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(product);
        }
    }
}
