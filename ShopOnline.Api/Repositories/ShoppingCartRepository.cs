using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext dbContext;

        public ShoppingCartRepository(ShopOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private async Task<bool> CartItemxists(int cartId, int productId)
        {
            return await dbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }
        public async Task<CartItem?> AddItem(CartItemToAddDto item)
        {
            if(!await CartItemxists(item.CartId, item.ProductId))
            {
                var newItem = await (from product in dbContext.Products
                                  where product.Id == item.ProductId
                                  select new CartItem
                                  {
                                      CartId = item.CartId,
                                      ProductId = product.Id,
                                      Qty = item.Qty
                                  }).SingleOrDefaultAsync();
                if (newItem != null)
                {
                    var result = await dbContext.CartItems.AddAsync(newItem);
                    await dbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await dbContext.CartItems.FindAsync(id);
            if(item != null)
            {
                dbContext.CartItems.Remove(item);
                await dbContext.SaveChangesAsync();
            }
            return item;
        }

        public async Task<CartItem?> GetItem(int id)
        {
            return await (from cart in dbContext.Carts
                          join cartItem in dbContext.CartItems
                          on cart.Id equals cartItem.Id
                          where cartItem.Id == id
                          select cartItem).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in dbContext.Carts
                          join cartItem in dbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select cartItem).ToListAsync();
        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto item)
        {
            throw new NotImplementedException();
        }
    }
}
