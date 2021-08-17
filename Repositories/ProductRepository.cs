using API.Specification;
using Microsoft.EntityFrameworkCore;
using ShopNet.Data;
using ShopNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Specification
{
    public class ProductRepository : IGenericRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.
                Include("ProductType").  // alternative way with include 
                Include("ProductBrand").
                FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.
                Include(p =>p.ProductType).
                Include(p =>p.ProductBrand).
                ToListAsync();
        }
    }
}
