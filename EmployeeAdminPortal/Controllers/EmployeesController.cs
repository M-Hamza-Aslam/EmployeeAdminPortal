using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = _dbContext.Employees
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
                .ToList();

            Console.WriteLine(allEmployees);

            return Ok(allEmployees);
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
