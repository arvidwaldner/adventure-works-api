using AdventureWorks.Common.Domain.Products;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.Http.Constansts;
using AdventureWorks.Http.Responses.Products.v1;
using AdventureWorks.Service.Production;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Http.Controllers
{
    [Route($"{EndpointConstants.ProductionsUrl}/product-categories")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        /// <summary>
        ///GET: Returns all product categories.
        /// </summary>
        /// <returns>All product categories</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductCategoryResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductCategories()
        {
            var productCategoriesResults = await  _productCategoryService.GetProductCategories();
            var productCategoryResponseModels = MapProductCategoryResponseModels(productCategoriesResults);

            return Ok(productCategoryResponseModels);
        }

        /// <summary>
        ///GET: Returns a product category by Id.
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
            if (id <= 0)
                throw new BadRequestException($"Id must be positive integer");

            var productCategoryResult = await _productCategoryService.GetProductCategoryById(id);
            var productCategoryResponseModel = MapProductCategoryResponseModel(productCategoryResult);

            return Ok(productCategoryResponseModel);
        }

        /// <summary>
        /// OPTIONS: Allowed Http methods for the target resource
        /// </summary>
        /// <returns>Allowed Http methods in response header</returns>
        /// <response code="200">Ok</response>
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }

        private List<ProductCategoryResponseModel> MapProductCategoryResponseModels(List<ProductCategoryDto> productCategoryResults)
        {
            var results = new List<ProductCategoryResponseModel>();

            foreach (var productCategory in productCategoryResults)
            {
                results.Add(MapProductCategoryResponseModel(productCategory));
            }

            return results;
        }

        private ProductCategoryResponseModel MapProductCategoryResponseModel(ProductCategoryDto productCategoryResult)
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
