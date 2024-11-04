using AdventureWorks.Common.Domain.Production;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.Http.Constansts;
using AdventureWorks.Http.Requests.HumanResources.v1;
using AdventureWorks.Http.Requests.Production.v1;
using AdventureWorks.Http.Responses.Production.v1;
using AdventureWorks.Http.Responses.Products.v1;
using AdventureWorks.Service.Production;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Http.Controllers
{
    [Route($"{EndpointConstants.ProductionsUrl}/locations")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
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
            var locations = await _locationService.GetAllLocations();
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

            var location = await _locationService.GetLocationById(id);
            var locationResponseModel = MapLocationResponseModel(location);

            return Ok(locationResponseModel);
        }

        /// <summary>
        /// OPTIONS: Allowed Http methods for the target resource
        /// </summary>
        /// <returns>Allowed Http methods in response header</returns>
        /// <response code="200">Ok</response>
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        /// <summary>
        /// POST: Creates a new location
        /// </summary>
        /// <param name="request">Request for creating a new location</param>
        /// <returns>The created location</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(LocationResponseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLocation(CreateLocationRequestModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BadRequestException($"The property '{nameof(CreateLocationRequestModel)}.{nameof(request.Name)}' cannot be empty");

            var locationDto = new LocationDto 
            {
                Name = request.Name,
                Availability = request.Availability,
                CostRate = request.CostRate
            };

            var createdLocationDto = await _locationService.CreateLocation(locationDto);
            var locationResponseModel = MapLocationResponseModel(createdLocationDto);
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var createdLocationUrl = $"{baseUrl}/{EndpointConstants.ProductionsUrl}/locations/{locationResponseModel.LocationId}";
            return Created(createdLocationUrl, locationResponseModel);
        }

        /// <summary>
        /// PUT: Updates an existing location
        /// </summary>
        /// <param name="id">Id of existing location</param>
        /// <param name="request">Request to update existing location</param>
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLocation(int id, UpdateLocationRequestModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BadRequestException($"The property '{nameof(CreateLocationRequestModel)}.{nameof(request.Name)}' cannot be empty");

            var locationDto = new LocationDto
            {
                Name = request.Name,
                Availability = request.Availability,
                CostRate = request.CostRate
            };

            await _locationService.UpdateLocation(id, locationDto);
            return NoContent();
        }

        /// <summary>
        /// DELETE: Removes an existing location
        /// </summary>
        /// <param name="id">Id of existing location</param>
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocation(id);
            return NoContent();
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
