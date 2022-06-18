﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkinetApi.Contracts;
using SkinetApi.Dtos;
using SkinetApi.Entities;
using SkinetApi.Specification;

namespace SkinetApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> brandsRepo;
        private readonly IGenericRepository<ProductType> typesRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> productRepo, 
            IGenericRepository<ProductBrand> brandsRepo, 
            IGenericRepository<ProductType> typesRepo, IMapper mapper)
        {
            this.productRepo = productRepo;
            this.brandsRepo = brandsRepo;
            this.typesRepo = typesRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var specs = new ProductWithTypesandBrandsSpecification();


            var product = await productRepo.ListAsync(specs);

            var productMap = mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(product);
            return Ok(productMap);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductWithTypesandBrandsSpecification(id);

            var product = await productRepo.GetEntityWithSpec(spec);

            var producttoreturn = mapper.Map<Product, ProductToReturnDto>(product);

            return Ok(producttoreturn);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductBrands()
        {
            var productBrands = await brandsRepo.GetAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            var productTypes = await typesRepo.GetAllAsync();
               

            return Ok(productTypes);
        }


    }
}
