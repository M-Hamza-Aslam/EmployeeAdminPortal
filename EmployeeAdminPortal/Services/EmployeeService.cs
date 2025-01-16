using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Dtos.Common;
using EmployeeAdminPortal.Dtos.Employee;
using EmployeeAdminPortal.DTOs.Employee;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Repositories;

namespace EmployeeAdminPortal.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly EmployeeRepo _employeeRepo;

        public EmployeeService(ApplicationDbContext dbContext, EmployeeRepo employeeRepo)
        {
            _dbContext = dbContext;
            _employeeRepo = employeeRepo;
        }

        public PaginatedResponseDto<EmployeeResponseDto> GetAll(
            string? searchText,
            string? sortField,
            string? sortOrder,
            int pageNumber,
            int pageSize)
        {
            var allEmployeesQuery = _employeeRepo.GetAll(
                searchText,
                sortField,
                sortOrder);

            // Paging
            var totalRecords = allEmployeesQuery.Count();

            var employees = allEmployeesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    Office = new EmployeeOfficeDto
                    {
                        Id = e.Office.Id,
                        Name = e.Office.Name,
                        City = e.Office.City,
                        Country = e.Office.Country
                    },
                    Projects = e.Projects.Select(p => new EmployeeProjectDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                    }).ToList()
                })
                .ToList();

            return new PaginatedResponseDto<EmployeeResponseDto>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalRecords = totalRecords,
                Data = employees
            };
        }

        public EmployeeResponseDto? GetById(Guid id)
        {
            var employee = _employeeRepo.GetById(id);

            if (employee is null) return null;

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                Office = new EmployeeOfficeDto
                {
                    Id = employee.Office.Id,
                    Name = employee.Office.Name,
                    City = employee.Office.City,
                    Country = employee.Office.Country
                },
                Projects = employee.Projects.Select(p => new EmployeeProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                }).ToList()
            };
        }

        public Guid? Create(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
                OfficeId = addEmployeeDto.OfficeId,
            };

            var employeeId = _employeeRepo.Create(employeeEntity);

            return employeeId;
        }

        public Guid? Update(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = _employeeRepo.GetById(id);

            if (employee is null)
            {
                return null;
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;
            employee.OfficeId = updateEmployeeDto.OfficeId;

            var employeeId = _employeeRepo.Update(employee);

            return employeeId;
        }

        public Guid? DeleteEmployeeService(Guid id)
        {
            var employee = _employeeRepo.GetById(id);

            if (employee is null)
            {
                return null;
            }

            var employeeId = _employeeRepo.Remove(employee);

            return employeeId;
        }

    }
}
