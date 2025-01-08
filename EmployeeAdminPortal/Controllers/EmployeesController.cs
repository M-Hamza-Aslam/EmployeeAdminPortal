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
            var AllEmployees = _employeeService.GetAllEmployeesService(searchText, sortField, sortOrder, pageNumber, pageSize);

            return Ok(AllEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
          var employee = _employeeService.GetEmployeeByIdService(id);

            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = _employeeService.CreateEmployeeService(addEmployeeDto);

            return CreatedAtAction("CreateEmployee", employeeEntity);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = _employeeService.UpdateEmployeeService(id, updateEmployeeDto);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _employeeService.DeleteEmployeeService(id);

            if (!employee)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
