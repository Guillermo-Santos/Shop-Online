using ShopOnline.Models.Dtos;

namespace ShopOnline.WebServer.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int id);
    }
}
