using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinetApi.Contracts;
using SkinetApi.Entities;

namespace SkinetApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> brandsRepo;
        private readonly IGenericRepository<ProductType> typesRepo;

        public ProductController(IGenericRepository<Product> productRepo, 
            IGenericRepository<ProductBrand> brandsRepo, 
            IGenericRepository<ProductType> typesRepo)
        {
            this.productRepo = productRepo;
            this.brandsRepo = brandsRepo;
            this.typesRepo = typesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var product = await productRepo.GetAllAsync();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await productRepo.GetByIdAsync(id);
            return Ok(product);
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
