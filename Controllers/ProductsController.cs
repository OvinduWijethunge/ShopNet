using API.DTOs;
using API.Specification;
using AutoMapper;
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
        private readonly IGenericRepository<Product> _productRepo;
        private IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public  async Task< ActionResult<IReadOnlyList<ProductDTO>>> GetProducts()
        {
            var spec = new ProductWithTypeAndBrandSpecification();
            var products_list = await _productRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products_list));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id) 
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var _product =  await _productRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductDTO>(_product);
        } 
    }
}
