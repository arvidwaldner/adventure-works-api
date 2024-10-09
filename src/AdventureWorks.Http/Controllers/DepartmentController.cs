using AdventureWorks.Common.Domain.HumanResources;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.Http.Constansts;
using AdventureWorks.Http.Requests.HumanResources.v1;
using AdventureWorks.Http.Responses.HumanResources.v1;
using AdventureWorks.Http.Responses.Production.v1;
using AdventureWorks.Service.HumanResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Http.Controllers
{
    [Route($"{EndpointConstants.HumanResourcesUrl}/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        ///GET: Returns all departments.
        /// </summary>
        /// <returns>All departments</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<DepartmentResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            var departmentResponseModels = MapDepartmentResponseModels(departments);

            return Ok(departmentResponseModels);
        }

        /// <summary>
        ///GET: Returns a department by Id.
        /// </summary>
        /// <param name="id">The department Id.</param>
        /// <returns>The department with the specified Id.</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DepartmentResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLocationById(int id)
        {
            if (id <= 0)
                throw new BadRequestException($"Id must be positive integer");

           var department = await _departmentService.GetDepartmentById(id);
           var departmentResponseModel = MapDepartmentResponseModel(department);

            return Ok(departmentResponseModel);
        }

        /// <summary>
        ///POST: Creates a new department.
        /// </summary>
        /// <returns>The created department</returns>
        /// <param name="request">Request for creating a new department</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(DepartmentResponseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentRequestModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BadRequestException($"The property '{nameof(CreateDepartmentRequestModel)}.{nameof(request.Name)}' cannot be empty");

            if(string.IsNullOrWhiteSpace(request.GroupName))
                throw new BadRequestException($"The property '{nameof(CreateDepartmentRequestModel)}.{nameof(request.GroupName)}' cannot be empty");

            var departmentDto = new DepartmentDto 
            {
                GroupName = request.GroupName,
                Name = request.Name
            };

            var createdDepartment = await _departmentService.CreateDepartment(departmentDto);
            var createdDepartmentResponseModel = MapDepartmentResponseModel(createdDepartment);
            var createdDepartmentUrl = $"{EndpointConstants.HumanResourcesUrl}/departments/{createdDepartmentResponseModel.DepartmentId}";

            return Created(createdDepartmentUrl, createdDepartmentResponseModel);
        }

        /// <summary>
        ///PUT: Updates an existing department.
        /// </summary>
        /// <param name="id">Id of the existing department to update</param>
        /// <param name="request">Request for updating existing department</param>
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
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentRequestModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BadRequestException($"The property '{nameof(CreateDepartmentRequestModel)}.{nameof(request.Name)}' cannot be empty");

            if (string.IsNullOrWhiteSpace(request.GroupName))
                throw new BadRequestException($"The property '{nameof(CreateDepartmentRequestModel)}.{nameof(request.GroupName)}' cannot be empty");

            var departmentDto = new DepartmentDto
            {
                GroupName = request.GroupName,
                Name = request.Name
            };

            await _departmentService.UpdateDepartment(id, departmentDto);
            return NoContent();
        }

        /// <summary>
        /// DELETE: Removes an existing department.
        /// </summary>
        /// <param name="id">Id of the existing department</param>
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (id <= 0)
                throw new BadRequestException($"Id must be positive integer");

            await _departmentService.DeleteDepartment(id);
            return NoContent();
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


        private List<DepartmentResponseModel> MapDepartmentResponseModels(List<DepartmentDto> departments)
        {
            var result = new List<DepartmentResponseModel>();
            foreach (var department in departments)
            {
                result.Add(MapDepartmentResponseModel(department));
            }

            return result;
        }

        private DepartmentResponseModel MapDepartmentResponseModel(DepartmentDto department)
        {
            var result = new DepartmentResponseModel
            {
                DepartmentId = department.DepartmentId,
                GroupName = department.GroupName,
                ModifiedDate = department.ModifiedDate,
                Name = department.Name
            };

            return result;
        }
    }
}
