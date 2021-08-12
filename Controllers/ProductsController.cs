using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopNet.Data;
using ShopNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public  async Task< ActionResult<List<Product>>> GetProducts()
        {
            var products_list = await _context.Products.ToListAsync();
            return Ok(products_list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) => await _context.Products.FindAsync(id);
    }
}
