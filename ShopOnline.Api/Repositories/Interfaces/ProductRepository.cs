using ShopOnline.Api.Entity;

namespace ShopOnline.Api.Repositories.Interfaces
{
    public interface ProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<Product> GetCategory(int id);

    }
}
