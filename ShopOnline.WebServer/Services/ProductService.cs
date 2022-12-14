using ShopOnline.Models.Dtos;
using ShopOnline.WebServer.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.WebServer.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }

                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Product");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }

                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
