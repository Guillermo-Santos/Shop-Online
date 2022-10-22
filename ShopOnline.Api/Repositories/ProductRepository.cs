using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;

namespace ShopOnline.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext dbContext;

        public ProductRepository(ShopOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await dbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await dbContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<ProductCategory> GetProductCategoryById(int id)
        {
            var category = await dbContext.ProductCategories.Where(c => c.Id == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await this.dbContext.Products.ToListAsync();
            return products;
        }
    }
}
