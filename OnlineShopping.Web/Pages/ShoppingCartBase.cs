using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OnlineShopping.Models.Dtos;
using OnlineShopping.Web.Services.Contracts;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace OnlineShopping.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }

        protected string? TotalPrice { get; set; } = string.Empty;
        protected int TotalQuantity { get; set; } = 0;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CalculateCartSummary();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected async Task DeleteCartItem_click(int id)
        {
            var cartItems = await ShoppingCartService.DeleteItem(id);

            RemoveCartItem(id);
            CalculateCartSummary();

        }




        private CartItemDto? GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDto);
        }
        protected async void UpdateCartItem_click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        Qty = qty,
                        CartItemId = id
                    };
                    var returnedUpdatedItemDto = await ShoppingCartService.UpdateQty(updateItemDto);
                    UpdateItemTotalPrice(returnedUpdatedItemDto);
                    CalculateCartSummary();
                    StateHasChanged();
                    await MakeUpdateVisible(id, false);
                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(a => a.Id == id);

                    if (item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected async void updateQty(int id)
        {
            await MakeUpdateVisible(id, true);
        }

        protected async Task MakeUpdateVisible(int id, bool visible)
        {
            await JSRuntime.InvokeVoidAsync("MakeUpdateVisible", id, visible);
        }
        private void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }

        }

        private void CalculateCartSummary()
        {
            setTotalPrice();
            setTotalQuantity();
        }

        private void setTotalPrice()
        {
            TotalPrice = ShoppingCartItems.Sum(a => a.TotalPrice).ToString("C");
        }

        private void setTotalQuantity()
        {
            TotalQuantity = ShoppingCartItems.Sum(a => a.Qty);
        }

    }
}
