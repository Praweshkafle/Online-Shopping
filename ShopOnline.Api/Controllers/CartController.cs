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
        [Route("getitems/{userid:int}")]
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
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int userid)
        {
            try
            {
                var cartItems = await this.shoppingCartRepository.GetItem(userid);
                if (cartItems == null)
                {
                    return NoContent();
                }
                var products = await this.productRepository.GetItem(cartItems.ProductId);
                if (products == null)
                {
                    throw new Exception("no products");
                }

                return Ok(cartItems.ConvertToDto(products));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var newCartItem = await this.shoppingCartRepository.AddItem(cartItemToAddDto);
                if (newCartItem == null) { return NoContent(); }
                var product = await productRepository.GetItem(newCartItem.ProductId);
                if (product == null)
                {
                    throw new Exception("Something went wrong");
                }

                var newCartItemDto = newCartItem.ConvertToDto(product);
                return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
                var cartitem = await shoppingCartRepository.DeleteItem(id);
                if (cartitem == null)
                {
                    return NotFound();
                }

                var product = await productRepository.GetItem(cartitem.ProductId);

                if (product == null) return NotFound();

                var cartitemDto = cartitem.ConvertToDto(product);

                return Ok(cartitemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartItemDto>> UpdateQty(int id,
            CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var cartitem = await shoppingCartRepository.UpdateQty(id, cartItemQtyUpdateDto);
                if (cartitem == null)
                {
                    return NotFound();
                }

                var product = await productRepository.GetItem(cartitem.ProductId);

                if (product == null) return NotFound();

                var cartitemDto = cartitem.ConvertToDto(product);

                return Ok(cartitemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
