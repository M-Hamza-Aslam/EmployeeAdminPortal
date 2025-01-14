using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.DTOs.Office;
using EmployeeAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly OfficeService _officeService;

        public OfficesController(ApplicationDbContext dbContext, OfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpGet]
        public IActionResult GetAllOffices()
        {

            var offices = _officeService.GetAll();
            return Ok(offices);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOfficeById(int id)
        {
            var office = _officeService.GetById(id);

            if (office is null)
            {
                return NotFound();
            }
            return Ok(office);
        }

        [HttpPost]
        public IActionResult CreateOffice(AddOfficeDto office)
        {

            var newOfficeId = _officeService.Create(office);

            if (newOfficeId is null)
            {
                return BadRequest();
            }

            return Ok(newOfficeId);

        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateOffice(int id, UpdateOfficeDto office)
        {
            var UpdatedOfficeId = _officeService.Update(id, office);
            if (UpdatedOfficeId is null)
            {
                return NotFound();
            }

            return Ok(UpdatedOfficeId);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteOffice(int id)
        {
            var office = _officeService.Delete(id);
            if (office is null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
