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

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await _context.ProductCategories.SingleOrDefaultAsync(a => a.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
    }
}
