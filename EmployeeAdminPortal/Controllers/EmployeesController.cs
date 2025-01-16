using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Dtos.Common;
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
        public IActionResult GetAllEmployees([FromQuery] PaginatedRequestDto Inputs)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //service
            var AllEmployees = _employeeService.GetAll(Inputs.SearchText, Inputs.SortField, Inputs.SortOrder, Inputs.PageNumber, Inputs.PageSize);

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
