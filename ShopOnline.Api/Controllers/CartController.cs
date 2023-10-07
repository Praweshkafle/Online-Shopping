using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models.Dtos;
using ShopOnline.Api.Entity;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Interfaces;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ShoppingCartRepository shoppingCartRepository;
        private readonly ProductRepository productRepository;

        public CartController(ShoppingCartRepository shoppingCartRepository
            , ProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("{userid}/getitems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userid)
        {
            try
            {
                var cartItems = await this.shoppingCartRepository.GetItems(userid);
                if (cartItems == null)
                {
                    return NoContent();
                }
                var products = await this.productRepository.GetItems();
                if (products == null)
                {
                    throw new Exception("no products");
                }

                return Ok(cartItems.ConvertToDto(products));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
