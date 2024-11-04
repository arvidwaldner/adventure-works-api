using AdventureWorks.Common.Domain.Products;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Service.Production
{
    public interface IProductCategoryService
    {
        Task<List<ProductCategoryDto>> GetProductCategories();
        Task <ProductCategoryDto> GetProductCategoryById(int id);
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<List<ProductCategoryDto>> GetProductCategories()
        {
            var productCategories = await _productCategoryRepository.GetAllAsync();
            var productCategoryResults = MapProductCategoryResults(productCategories.ToList());

            return productCategoryResults;
        }

        public async Task<ProductCategoryDto> GetProductCategoryById(int id)
        {
            var productCategory = await FindProductCategory(id);
            var productCategoryResult = MapProductCategoryResult(productCategory);
            return productCategoryResult;
        }

        private async Task<ProductCategory> FindProductCategory(int id)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);

            if (productCategory == null)
                throw new NotFoundException($"The product category with Id '{id}' was not found.");

            return productCategory;
        }

        private List<ProductCategoryDto> MapProductCategoryResults(List<ProductCategory> productCategories)
        {
            var results = new List<ProductCategoryDto>();

            foreach (var productCategory in productCategories)
            {
                results.Add(MapProductCategoryResult(productCategory));
            }

            return results;
        }

        private ProductCategoryDto MapProductCategoryResult(ProductCategory productCategory)
        {
            var result = new ProductCategoryDto()
            {
                ModifiedDate = productCategory.ModifiedDate,
                Name = productCategory.Name,
                ProductCategoryId = productCategory.ProductCategoryId,
                Rowguid = productCategory.Rowguid
            };

            return result;
        }
    }
}
