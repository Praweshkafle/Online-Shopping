using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models.Dtos;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Interfaces;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly ProductRepository _productRepository;
        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products= await _productRepository.GetItems();
                var productCategories= await _productRepository.GetCategories();

                if (products== null || productCategories == null) 
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto(productCategories);
                    return Ok(productDtos);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                 "Error Retriving Datas");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await _productRepository.GetItem(id);
                if(product == null) { return NotFound(); }
                else
                {
                    var categoty = await _productRepository.GetCategory(product.CategoryId);
                    return Ok(product.ConvertToDto(categoty));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                                 "Error Retriving Datas");
            }
        }
    }
}
