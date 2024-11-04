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
    public interface IProductsService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
    }

    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _productsRepository;

        public ProductsService(IProductRepository productRepository)
        {
            _productsRepository = productRepository;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var productEntities = await _productsRepository.GetAllAsync();
            var productResults = MapProducts(productEntities.ToList());
            return productResults;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var productEntity = await FindProduct(id);
            var productResult = MapProduct(productEntity);
            return productResult;
        }

        private async Task<Product> FindProduct(int id) 
        {
            var productEntity = await _productsRepository.GetByIdAsync(id);

            if (productEntity == null)
                throw new NotFoundException($"Product with Id '{id}' was not found.");

            return productEntity;
        }

        private List<ProductDto> MapProducts(List<Product> productEntities)
        {
            var result = new List<ProductDto>();

            foreach (var productEntity in productEntities)
            {
                result.Add(MapProduct(productEntity));
            }

            return result;
        }

        private ProductDto MapProduct(Product productEntity)
        {
            var productResult = new ProductDto
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
