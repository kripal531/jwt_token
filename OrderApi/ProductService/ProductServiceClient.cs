using OrderApi.Dto;

namespace OrderApi.ProductService
{
    public class ProductServiceClient
    {
        private readonly HttpClient _http;
        public ProductServiceClient(HttpClient http) 
        { 
            _http = http;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            return await _http.GetFromJsonAsync<ProductDto>($"https://localhost:7284/api/Product/{id}");
        } 
    }
}
