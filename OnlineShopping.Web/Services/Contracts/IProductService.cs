using OnlineShopping.Models.Dtos;

namespace OnlineShopping.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
    }
}
