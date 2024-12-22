using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        private static Expression<Func<Employee, object>> GetSortExpression(string? sortField)
        {
            return sortField?.ToLower() switch
            {
                "email" => e => e.Email,
                "salary" => e => e.Salary,
                _ => e => e.Name // Default to Name
            };
        }

        public EmployeesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("find")]
        public IActionResult GetAllEmployees(
            string? searchText,
            string? sortField = "Name",
            string? sortOrder = "asc",
            int pageNumber = 1,
            int pageSize = 10
            )
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
                .Select(e => new 
                {
                    e.Id,
                    e.Name,
                    e.Email,
                    e.phone,
                    e.Salary,
                    Office = new
                    {
                        e.Office.Id,
                        e.Office.Name,
                        e.Office.City,
                        e.Office.Country
                    }
                })
                .ToList();

            return Ok(new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = employees
            });
        }


        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = _dbContext.Employees
                .Include(e => e.Office)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Email,
                    e.phone,
                    e.Salary,
                    Office = new
                    {
                        e.Office.Id,
                        e.Office.Name,
                        e.Office.City,
                        e.Office.Country
                    }
                })
                .FirstOrDefault(e => e.Id == id);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        [HttpPost]
        public IActionResult CreateEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                phone = addEmployeeDto.phone,
                Salary = addEmployeeDto.Salary,
                OfficeId = addEmployeeDto.OfficeId,
            };

            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();

            return CreatedAtAction("CreateEmployee", employeeEntity);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.phone = updateEmployeeDto.phone;
            employee.Salary = updateEmployeeDto.Salary;
            employee.OfficeId = updateEmployeeDto.OfficeId;

            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();

            return Ok(employee);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
