using AdventureWorks.Common.Domain.HumanResources;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.Http.Constansts;
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
        /// Returns all departments.
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
        /// Returns a department by Id.
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
