using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkinetApi.Contracts;
using SkinetApi.Dtos;
using SkinetApi.Entities;
using SkinetApi.Errors;
using SkinetApi.Specification;

namespace SkinetApi.Controllers
{
    public class ProductController : BaseApiController
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductWithTypesandBrandsSpecification(id);

            var product = await productRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

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
