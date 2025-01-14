using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.DTOs.Employee;
using EmployeeAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(ApplicationDbContext dbContext, EmployeeService employeeService)
        {
            _employeeService = employeeService;
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
            //service
            var AllEmployees = _employeeService.GetAll(searchText, sortField, sortOrder, pageNumber, pageSize);

            return Ok(AllEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = _employeeService.GetById(id);

            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(AddEmployeeDto addEmployeeDto)
        {
            var newEmployeeId = _employeeService.Create(addEmployeeDto);

            if (newEmployeeId is null)
            {
                return BadRequest();
            }


            return CreatedAtAction("CreateEmployee", newEmployeeId);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employeeId = _employeeService.Update(id, updateEmployeeDto);

            if (employeeId is null)
            {
                return NotFound();
            }

            return Ok(employeeId);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employeeId = _employeeService.DeleteEmployeeService(id);

            if (employeeId is null)
            {
                return NotFound();
            }

            return Ok(employeeId);
        }
    }
}
