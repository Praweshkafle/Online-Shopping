using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entity;
using ShopOnline.Api.Repositories.Interfaces;

namespace ShopOnline.Api.Repositories.Implementations
{
    public class ProductRepositoryImpl : ProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepositoryImpl(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories= await _context.ProductCategories.ToListAsync();
            return categories;
        }

        public Task<Product> GetCategory(int id)
        {
            return default;
        }

        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
    }
}
