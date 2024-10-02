using AdventureWorks.Common.Domain.Products;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.Http.Responses.Products.v1;
using AdventureWorks.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService) 
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id) 
        {
            try
            {
                var productResult = await _productsService.GetProductById(id);
                var productResponseModel = MapProductResponseModel(productResult);

                return Ok(productResponseModel);
            }
            catch(NotFoundException ex) 
            {
                return NotFound(ex);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex);
            }           
        }

        private ProductResponseModel MapProductResponseModel(ProductResult productResult)
        {
            var productResponseModel = new ProductResponseModel
            {
                SafetyStockLevel = productResult.SafetyStockLevel,
                ListPrice = productResult.ListPrice,
                MakeFlag = productResult.MakeFlag,
                Class = productResult.Class,
                Color = productResult.Color,
                ProductLine = productResult.ProductLine,
                ModifiedDate = productResult.ModifiedDate,
                SellEndDate = productResult.SellEndDate,
                DaysToManufacture = productResult.DaysToManufacture,
                Name = productResult.Name,
                DiscontinuedDate = productResult.DiscontinuedDate,
                FinishedGoodsFlag = productResult.FinishedGoodsFlag,
                ProductModelId = productResult.ProductModelId,
                SizeUnitMeasureCode = productResult.SizeUnitMeasureCode,
                WeightUnitMeasureCode = productResult.WeightUnitMeasureCode,
                ProductId = productResult.ProductId,
                ProductNumber = productResult.ProductNumber,
                ProductSubcategoryId = productResult.ProductSubcategoryId,
                SellStartDate = productResult.SellStartDate,
                Size = productResult.Size,
                ReorderPoint = productResult.ReorderPoint,
                Rowguid = productResult.Rowguid,
                StandardCost = productResult.StandardCost,
                Style = productResult.Style,
                Weight = productResult.Weight
            };

            return productResponseModel;
        }
    }
}
