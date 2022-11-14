using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem?> AddItem(CartItemToAddDto item);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto item);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem?> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);
    }
}
