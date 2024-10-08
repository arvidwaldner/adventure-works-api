using AdventureWorks.Common.Domain.Production;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.Http.Constansts;
using AdventureWorks.Http.Responses.Production.v1;
using AdventureWorks.Http.Responses.Products.v1;
using AdventureWorks.Service.Production;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Http.Controllers
{
    [Route($"{EndpointConstants.ProductionsUrl}/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        ///GET: Returns all locations.
        /// </summary>
        /// <returns>All locations</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<LocationResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLocations()
        {
            var locations = _locationService.GetAllLocations();
            var locationResponseModels = MapLocationResponseModels(locations);

            return Ok(locationResponseModels);
        }

        /// <summary>
        ///GET: Returns a location by Id.
        /// </summary>
        /// <param name="id">The location Id.</param>
        /// <returns>The location with the specified Id.</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocationResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLocationById(int id)
        {
            if (id <= 0)
                throw new BadRequestException($"Id must be positive integer");

            var location = _locationService.GetLocationById(id);
            var locationResponseModel = MapLocationResponseModel(location);

            return Ok(locationResponseModel);
        }

        private List<LocationResponseModel> MapLocationResponseModels(List<LocationDto> locations)
        {
            var result = new List<LocationResponseModel>();

            foreach (var location in locations)
            {
                result.Add(MapLocationResponseModel(location));
            }

            return result;
        }

        private LocationResponseModel MapLocationResponseModel(LocationDto location)
        {
            var locationResult = new LocationResponseModel
            {
                LocationId = location.LocationId,
                Availability = location.Availability,
                CostRate = location.CostRate,
                ModifiedDate = location.ModifiedDate,
                Name = location.Name
            };

            return locationResult;
        }
    }
}
