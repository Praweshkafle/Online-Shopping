using Microsoft.AspNetCore.Components;
using OnlineShopping.Models.Dtos;
using OnlineShopping.Web.Services.Contracts;

namespace OnlineShopping.Web.Pages
{
    public class ProductBase : ComponentBase
    {
        [Inject]
        public IProductService productService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await productService.GetItems();
                var shoppintCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                var totalQty = shoppintCartItems.Sum(a => a.Qty);

                ShoppingCartService.RaiseEventOnCartChanged(totalQty);

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductByCategory()
        {
            return Products.GroupBy(a => a.CategoryId)
                           .OrderBy(s => s.Key);
        }

        protected string GetCategoryName(IGrouping<int,ProductDto> groupedPtoductDto)
        {
            return Products.FirstOrDefault(s => s.CategoryId == groupedPtoductDto.Key).CategoryName;
        }
    }
}
