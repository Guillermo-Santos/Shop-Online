using ShopOnline.Api.Entities;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetProductById(int id);
        Task<ProductCategory> GetProductCategoryById(int id);
    }
}
