using ShopOnline.Models.Dtos;

namespace ShopOnline.WebServer.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartitemDto>> GetItems(int userId);
        Task<CartitemDto> AddItem(CartItemToAddDto item);
        Task<CartitemDto> DeleteItem(int id);
    }
}
