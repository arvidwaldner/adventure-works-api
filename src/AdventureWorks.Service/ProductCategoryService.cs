using AdventureWorks.Common.Domain.Products;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Service
{
    public interface IProductCategoryService
    {
        List<ProductCategoryResult> GetProductCategories();
        ProductCategoryResult GetProductCategoryById(int id);
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public List<ProductCategoryResult> GetProductCategories()
        {
            var productCategories = _productCategoryRepository.GetAll().ToList();
            var productCategoryResults = MapProductCategoryResults(productCategories);

            return productCategoryResults;
        }

        public ProductCategoryResult GetProductCategoryById(int id)
        {
            var productCategory = _productCategoryRepository.GetById(id);

            if (productCategory == null)
                throw new NotFoundException($"The product category with Id '{id}' was not found.");

            var productCategoryResult = MapProductCategoryResult(productCategory);
            return productCategoryResult;
        }

        private List<ProductCategoryResult> MapProductCategoryResults(List<ProductCategory> productCategories)
        {
            var results = new List<ProductCategoryResult>();

            foreach (var productCategory in productCategories) 
            {
                results.Add(MapProductCategoryResult(productCategory));
            }

            return results;
        }

        private ProductCategoryResult MapProductCategoryResult(ProductCategory productCategory)
        {
            var result = new ProductCategoryResult()
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
