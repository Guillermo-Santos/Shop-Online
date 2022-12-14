using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.WebServer.Services.Contracts;

namespace ShopOnline.WebServer.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetProducts();
        }
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProducts)
        {
            return groupedProducts.First(pg => pg.CategoryId == groupedProducts.Key).CategoryName;
        }
    }
}
