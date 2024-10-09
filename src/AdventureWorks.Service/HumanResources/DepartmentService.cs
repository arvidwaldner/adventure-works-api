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
        Task<DepartmentDto> CreateDepartment(DepartmentDto departmentDto);
        Task UpdateDepartment(int id, DepartmentDto departmentDto);
        Task DeleteDepartment(int id);
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
            var department = FindDepartment(id);
            var result = MapDepartmentDto(department);
            return result;
        }

        public async Task<DepartmentDto> CreateDepartment(DepartmentDto departmentDto)
        {
            var departmentEntity = new Department 
            {
                GroupName = departmentDto.GroupName,
                Name = departmentDto.Name
            };

            var createdDepartment = _repository.Insert(departmentEntity);
            var result = MapDepartmentDto(createdDepartment);
            return result;
        }

        public async Task UpdateDepartment(int id, DepartmentDto departmentDto)
        {
            var departmentToUpdate = FindDepartment(id);

            var changed = false;
            if(departmentToUpdate.Name != departmentDto.Name)
            {
                departmentToUpdate.Name = departmentDto.Name;
                changed = true;
            }

            if(departmentToUpdate.GroupName != departmentDto.GroupName)
            {
                departmentToUpdate.GroupName = departmentDto.GroupName;
                changed = true;
            }

            if(changed)
            {
                departmentToUpdate.ModifiedDate = DateTime.Now;
                _repository.Update(departmentToUpdate);
            }            
        }

        public async Task DeleteDepartment(int id)
        {
            var departmentToDelete = FindDepartment(id);
            _repository.Delete(departmentToDelete.DepartmentId);
        }

        private Department FindDepartment(int id)
        {
            var shortId = (short)id;
            var department = _repository.GetById(shortId);

            if (department == null)
                throw new NotFoundException($"Department with id: '{id}', was not found");

            return department;
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
