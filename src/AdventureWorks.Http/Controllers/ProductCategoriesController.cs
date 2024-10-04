using AdventureWorks.Common.Domain.Products;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.Http.Responses.Products.v1;
using AdventureWorks.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Http.Controllers
{
    [Route("adventure-works/api/product-categories")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Returns all product categories.
        /// </summary>
        /// <returns>All product categoris</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductCategoryResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductCategories()
        {
            var productResults =  _productCategoryService.GetProductCategories();
            var productCategoryResponseModels = MapProductCategoryResponseModels(productResults);

            return Ok(productCategoryResponseModels);
        }

        /// <summary>
        /// Returns a product category by Id.
        /// </summary>
        /// <param name="id">The product category Id.</param>
        /// <returns>The product category with the specified Id.</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductCategoryResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            var productResult = _productCategoryService.GetProductCategoryById(id);
            var productCategoryResponseModel = MapProductCategoryResponseModel(productResult);

            return Ok(productCategoryResponseModel);
        }

        private List<ProductCategoryResponseModel> MapProductCategoryResponseModels(List<ProductCategoryResult> productCategoryResults)
        {
            var results = new List<ProductCategoryResponseModel>();

            foreach (var productCategory in productCategoryResults)
            {
                results.Add(MapProductCategoryResponseModel(productCategory));
            }

            return results;
        }

        private ProductCategoryResponseModel MapProductCategoryResponseModel(ProductCategoryResult productCategoryResult)
        {
            var result = new ProductCategoryResponseModel()
            {
                ModifiedDate = productCategoryResult.ModifiedDate,
                Name = productCategoryResult.Name,
                ProductCategoryId = productCategoryResult.ProductCategoryId,
                Rowguid = productCategoryResult.Rowguid
            };

            return result;
        }
    }
}
