using OnlineShopping.Models.Dtos;
using ShopOnline.Api.Entity;

namespace ShopOnline.Api.Repositories.Interfaces
{
    public interface ShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userid);
    }
}
