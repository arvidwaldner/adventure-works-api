using AdventureWorks.Common.Domain.Sales;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.Http.Constansts;
using AdventureWorks.Http.Responses.Production.v1;
using AdventureWorks.Http.Responses.Sales.v1;
using AdventureWorks.Service.Sales;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Http.Controllers
{
    [Route($"{EndpointConstants.SalesUrl}/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        /// <summary>
        ///GET: Returns all stores.
        /// </summary>
        /// <returns>All stores</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<StoreResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await _storeService.GetAllStores();
            var responseModels = MapStoreResponseModels(stores);

            return Ok(responseModels);
        }

        /// <summary>
        ///GET: Returns a store by business entity Id.
        /// </summary>
        /// <param name="id">The business entity Id.</param>
        /// <returns>The store with the specified business entity Id.</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoreResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStoreByBusinessEntityId(int id)
        {
            if (id <= 0)
                throw new BadRequestException($"Id must be positive integer");

            var store = await _storeService.GetStoreByBusinessEntityId(id);
            var storeResponseModel = MapStoreResponseModel(store);

            return Ok(storeResponseModel);
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

        private List<StoreResponseModel> MapStoreResponseModels(List<StoreDto> stores)
        {
            var storeResponseModels = new List<StoreResponseModel>();

            foreach (var store in stores)
            {
                storeResponseModels.Add(MapStoreResponseModel(store));
            }

            return storeResponseModels;
        }

        private StoreResponseModel MapStoreResponseModel(StoreDto store)
        {
            var storeResponseModel = new StoreResponseModel
            {
                BusinessEntityId = store.BusinessEntityId,
                Demographics = store.Demographics,
                Name = store.Name,
                ModifiedDate = store.ModifiedDate,
                RowGuid = store.RowGuid,
                SalesPersonId = store.SalesPersonId
            };

            return storeResponseModel;
        }
    }
}
