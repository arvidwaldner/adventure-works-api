using AdventureWorks.Common.Domain.Products;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.Http.Constansts;
using AdventureWorks.Http.Responses.Products.v1;
using AdventureWorks.Service.Production;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Http.Controllers
{
    [Route($"{EndpointConstants.ProductionsUrl}/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService) 
        {
            _productsService = productsService;
        }

        /// <summary>
        ///GET: Returns all products.
        /// </summary>
        /// <returns>All products</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {
            var productResults = await _productsService.GetAllProductsAsync();
            var productResponseModels = MapProductResponseModels(productResults);

            return Ok(productResponseModels);
        }

        /// <summary>
        ///GET: Returns a product by Id.
        /// </summary>
        /// <param name="id">The product Id.</param>
        /// <returns>The product with the specified Id.</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProduct(int id) 
        {
            if (id <= 0)
                throw new BadRequestException($"Id must be positive integer");

            var productResult = await _productsService.GetProductByIdAsync(id);
            var productResponseModel = MapProductResponseModel(productResult);

            return Ok(productResponseModel);
        }       
                
        private List<ProductResponseModel> MapProductResponseModels(List<ProductDto> productResults)
        {
            var productResponseModels = new List<ProductResponseModel>();

            foreach (var productResult in productResults) 
            {
                productResponseModels.Add(MapProductResponseModel(productResult));   
            }

            return productResponseModels;
        }

        private ProductResponseModel MapProductResponseModel(ProductDto productResult)
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
