using ShopOnline.Models.Dtos;
using ShopOnline.WebServer.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.WebServer.Services
{
    public sealed class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<CartitemDto> AddItem(CartItemToAddDto item)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/ShoppingCart", item);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<CartitemDto>();
                }

                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code:{response.StatusCode} Message: {message}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartitemDto> DeleteItem(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/ShoppingCart/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartitemDto>();
                }
                return default;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CartitemDto>> GetItems(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return Enumerable.Empty<CartitemDto>().ToList();
                    return await response.Content.ReadFromJsonAsync<List<CartitemDto>>();
                }

                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code:{response.StatusCode} Message: {message}");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
