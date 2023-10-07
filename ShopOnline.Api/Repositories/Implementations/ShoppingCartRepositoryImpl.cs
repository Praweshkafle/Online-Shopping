using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models.Dtos;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entity;
using ShopOnline.Api.Repositories.Interfaces;

namespace ShopOnline.Api.Repositories.Implementations
{
    public class ShoppingCartRepositoryImpl : ShoppingCartRepository
    {
        private readonly AppDbContext appDbContext;

        public ShoppingCartRepositoryImpl(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.appDbContext.CartItems.AnyAsync(a => a.Id == cartId && a.ProductId == productId);
        }


        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {

            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var productId = await this.appDbContext.Products.Where(a => a.Id == cartItemToAddDto.ProductId).SingleOrDefaultAsync();
                var item = new CartItem
                {
                    CartId = cartItemToAddDto.CartId,
                    ProductId = productId.Id,
                    Qty = cartItemToAddDto.Qty,
                };
                if (item != null)
                {
                    var result = await this.appDbContext.CartItems.AddAsync(item);
                    await this.appDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem?> GetItem(int? id)
        {
            return await (from cart in this.appDbContext.Carts
                          join cartItem in this.appDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userid)
        {
            return await (from cart in this.appDbContext.Carts
                         join cartItem in this.appDbContext.CartItems
                         on cart.Id equals cartItem.CartId
                         where cart.UserId == userid
                         select new CartItem
                         {
                             Id = cartItem.Id,
                             ProductId = cartItem.ProductId,
                             Qty = cartItem.Qty,
                             CartId = cartItem.CartId
                         }).ToListAsync();
        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
