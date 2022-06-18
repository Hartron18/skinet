using SkinetApi.Entities;
using System.Linq.Expressions;

namespace SkinetApi.Specification
{
    public class ProductWithTypesandBrandsSpecification: BaseSpecification<Product>
    {
        public ProductWithTypesandBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductWithTypesandBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
