using Microsoft.AspNetCore.Components;
using OnlineShopping.Models.Dtos;
using OnlineShopping.Web.Services.Contracts;

namespace OnlineShopping.Web.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
