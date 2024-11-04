using AdventureWorks.Common.Domain.HumanResources;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.HumanResources;
using AdventureWorks.Service.HumanResources;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;


namespace AdventureWorks.Service.Test
{
    [TestClass]
    public class DepartmentServiceTest
    {
        private DepartmentService _target;
        private Mock<IDepartmentRepository> _departmentRepositoryMock;

        [TestInitialize]
        public void Initialize()
        {
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _target = new DepartmentService(_departmentRepositoryMock.Object);
        }

        [TestCleanup]
        public void Cleanup() 
        {
            _departmentRepositoryMock.VerifyNoOtherCalls();
            
            _departmentRepositoryMock = null;
            _target = null;
        }

        [TestMethod]
        public async Task GetAllDepartments_ThreeExistingDepartments_ThreeDepartmentsReturned()
        {
            var date1 = CreateDate(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var date2 = CreateDate(DateTime.Today.Year - 1, DateTime.Today.Month - 1, DateTime.Today.Day - 1);
            var date3 = CreateDate(DateTime.Today.Year - 2, DateTime.Today.Month - 2, DateTime.Today.Day - 2);

            var departments = new List<Department> 
            {
                CreateDepartment("First", "FirstGroup", 1, date1),
                CreateDepartment("Second", "SecondGroup", 2, date2),
                CreateDepartment("Third", "ThirdGroup", 3, date3),
            };

            var expectedDepartmentDtos = new List<DepartmentDto> 
            {
                CreateDepartmentDto("First", "FirstGroup", 1, date1),
                CreateDepartmentDto("Second", "SecondGroup", 2, date2),
                CreateDepartmentDto("Third", "ThirdGroup", 3, date3),
            };

            _departmentRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(departments);

            var actualDepartmentDtos = await _target.GetAllDepartments();
            VerifyDepartments(expectedDepartmentDtos, actualDepartmentDtos);

            _departmentRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetDepartmentById_ThreeExistingDepartments_MatchingDepartmentId_MatchingDepartmentReturned()
        {
            var date1 = CreateDate(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var date2 = CreateDate(DateTime.Today.Year - 1, DateTime.Today.Month - 1, DateTime.Today.Day - 1);
            var date3 = CreateDate(DateTime.Today.Year - 2, DateTime.Today.Month - 2, DateTime.Today.Day - 2);

            var departments = new List<Department>
            {
                CreateDepartment("First", "FirstGroup", 1, date1),
                CreateDepartment("Second", "SecondGroup", 2, date2),
                CreateDepartment("Third", "ThirdGroup", 3, date3),
            };

            var matchingDepartment = departments[1];
            var expectedDepartmentDto = CreateDepartmentDto("Second", "SecondGroup", 2, date2);

            _departmentRepositoryMock.Setup(x => x.GetByIdAsync((short)2))
                .ReturnsAsync(matchingDepartment);

            var actualDepartmentDto = await _target.GetDepartmentById(2);
            VerifyDepartment(expectedDepartmentDto, actualDepartmentDto);

            _departmentRepositoryMock.Verify(x => x.GetByIdAsync((short)2), Times.Once);
        }

        [TestMethod]
        public async Task GetDepartmentById_ThreeExistingDepartments_NoMatchingDepartmentId_NotFoundExceptionThrown()
        {
            var date1 = CreateDate(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var date2 = CreateDate(DateTime.Today.Year - 1, DateTime.Today.Month - 1, DateTime.Today.Day - 1);
            var date3 = CreateDate(DateTime.Today.Year - 2, DateTime.Today.Month - 2, DateTime.Today.Day - 2);

            var departments = new List<Department>
            {
                CreateDepartment("First", "FirstGroup", 1, date1),
                CreateDepartment("Second", "SecondGroup", 2, date2),
                CreateDepartment("Third", "ThirdGroup", 3, date3),
            };

            _departmentRepositoryMock.Setup(x => x.GetByIdAsync((short)4))
                .ReturnsAsync((Department)null);

            Exception actual = new Exception();
            try
            {
                await _target.GetDepartmentById(4);
            }
            catch (Exception ex) 
            {
                actual = ex;
            }

            Assert.IsInstanceOfType<NotFoundException>(actual);
            Assert.IsNotNull(actual.Message);
            Assert.AreEqual("Department with id: '4', was not found", actual.Message);

            _departmentRepositoryMock.Verify(x => x.GetByIdAsync((short)4), Times.Once);
        }

        private void VerifyDepartments(List<DepartmentDto> expected, List<DepartmentDto> actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);

            for(var i = 0; i < expected.Count; i++)
            {
                VerifyDepartment(expected[i], actual[i]);
            }
        }

        private void VerifyDepartment(DepartmentDto expected, DepartmentDto actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.GroupName, actual.GroupName);
            Assert.AreEqual(expected.DepartmentId, actual.DepartmentId);
            Assert.AreEqual(expected.ModifiedDate, actual.ModifiedDate);            
        }

        private DateTime CreateDate(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        private DepartmentDto CreateDepartmentDto(string name, string groupName, short departmentId, DateTime modifiedDate)
        {
            return new DepartmentDto 
            {
                Name = name,
                GroupName = groupName,
                DepartmentId = departmentId,
                ModifiedDate = modifiedDate
            };
        }

        private Department CreateDepartment(string name, string groupName, short departmentId, DateTime modifiedDate)
        {
            return new Department
            {
                Name = name,
                GroupName = groupName,
                DepartmentId = departmentId,
                ModifiedDate = modifiedDate
            };
        }
    }
}