using AdventureWorks.Common.Domain.HumanResources;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Service.HumanResources
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllDepartments();
        Task<DepartmentDto> GetDepartmentById(int id);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DepartmentDto>> GetAllDepartments()
        {
            var departments = _repository.GetAll().ToList();
            var result = MapDepartmentDtos(departments);

            return result;
        }

        public async Task<DepartmentDto> GetDepartmentById(int id)
        {
            var shortId = (short)id;
            var department = _repository.GetById(shortId);

            if (department == null) 
            {
                throw new NotFoundException($"Department with id: '{id}', was not found");
            }

            var result = MapDepartmentDto(department);
            return result;
        }

        private List<DepartmentDto> MapDepartmentDtos(List<Department> departments)
        {
            var result = new List<DepartmentDto>();
            foreach (var department in departments) 
            {
                result.Add(MapDepartmentDto(department));
            }

            return result;
        }

        private DepartmentDto MapDepartmentDto(Department department) 
        {
            var result = new DepartmentDto 
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
