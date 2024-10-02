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
    public interface IProductsService
    {
        Task<ProductResult> GetProductById(int id);
    }

    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _productsRepository;

        public ProductsService(IProductRepository productRepository) 
        {
            _productsRepository = productRepository;
        }

        public async Task<ProductResult> GetProductById(int id)
        {
            var productEntity = _productsRepository.GetById(id);

            if (productEntity == null)
                throw new NotFoundException($"Product with Id '{id}' was not found.");

            var productResult = MapProduct(productEntity);
            return productResult;
        }

        private ProductResult MapProduct(Product productEntity)
        {
            var productResult = new ProductResult
            {
                ProductId = productEntity.ProductId,
                ListPrice = productEntity.ListPrice,
                ProductLine = productEntity.ProductLine,
                SafetyStockLevel = productEntity.SafetyStockLevel,
                Class = productEntity.Class,
                Color = productEntity.Color,
                DaysToManufacture = productEntity.DaysToManufacture,
                Name = productEntity.Name,
                DiscontinuedDate = productEntity.DiscontinuedDate,
                ProductNumber = productEntity.ProductNumber,
                MakeFlag = productEntity.MakeFlag,
                ModifiedDate = productEntity.ModifiedDate,
                SellEndDate = productEntity.SellEndDate,
                FinishedGoodsFlag = productEntity.FinishedGoodsFlag,
                SellStartDate = productEntity.SellStartDate,
                Size = productEntity.Size,
                ProductModelId = productEntity.ProductModelId,
                ProductSubcategoryId = productEntity.ProductSubcategoryId,
                ReorderPoint = productEntity.ReorderPoint,
                Rowguid = productEntity.Rowguid,
                SizeUnitMeasureCode = productEntity.SizeUnitMeasureCode,
                StandardCost = productEntity.StandardCost,
                WeightUnitMeasureCode = productEntity.WeightUnitMeasureCode,
                Style = productEntity.Style,
                Weight = productEntity.Weight
            };

            return productResult;
        }
    }
}
