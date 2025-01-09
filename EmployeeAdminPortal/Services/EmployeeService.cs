using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Dtos.Common;
using EmployeeAdminPortal.DTOs.Employee;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeAdminPortal.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private static Expression<Func<Employee, object>> GetSortExpression(string? sortField)
        {
            return sortField?.ToLower() switch
            {
                "email" => e => e.Email,
                "salary" => e => e.Salary,
                _ => e => e.Name // Default to Name
            };
        }

        public PaginatedResponseDto<Employee> GetAllEmployeesService(
            string? searchText,
            string? sortField,
            string? sortOrder,
            int pageNumber,
            int pageSize)
        {
            var allEmployeesQuery = _dbContext.Employees
           .Include(e => e.Office).AsQueryable();

            //searching
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                allEmployeesQuery = allEmployeesQuery.Where(e => e.Name.Contains(searchText) || e.Email.Contains(searchText) || e.Salary.ToString().Contains(searchText));
            }


            // Sorting
            allEmployeesQuery = sortOrder?.ToLower() == "desc"
            ? allEmployeesQuery.OrderByDescending(GetSortExpression(sortField))
                : allEmployeesQuery.OrderBy(GetSortExpression(sortField));

            // Paging
            var totalRecords = allEmployeesQuery.Count();

            var employees = allEmployeesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new Employee
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    Office = new Office
                    {
                        Id = e.Office.Id,
                        Name = e.Office.Name,
                        City = e.Office.City,
                        Country = e.Office.Country
                    }
                })
                .ToList();

            return new PaginatedResponseDto<Employee>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalRecords = totalRecords,
                Data = employees
            };
        }

        public Employee? GetEmployeeByIdService(Guid id)
        {
            var employee = _dbContext.Employees
            .Include(e => e.Office)
            .Select(e => new Employee
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                Salary = e.Salary,
                Office = new Office
                {
                    Id = e.Office.Id,
                    Name = e.Office.Name,
                    City = e.Office.City,
                    Country = e.Office.Country
                }
            })
            .FirstOrDefault(e => e.Id == id);

            return employee;
        }

        public Employee CreateEmployeeService(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.phone,
                Salary = addEmployeeDto.Salary,
                OfficeId = addEmployeeDto.OfficeId,
            };

            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();

            return employeeEntity;
        }

        public Employee? UpdateEmployeeService(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee is null)
            {
                return null;
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.phone;
            employee.Salary = updateEmployeeDto.Salary;
            employee.OfficeId = updateEmployeeDto.OfficeId;

            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();

            return employee;
        }

        public bool DeleteEmployeeService(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee is null)
            {
                return false;
            }

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();

            return true;
        }

    }
}
