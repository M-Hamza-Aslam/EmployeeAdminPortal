﻿using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.DTOs.Office;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OfficesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult getAllOffices()
        {

            var offices = _dbContext.Offices
                .Include(o => o.Employees)
                .Select(o => new
                {
                    o.Id,
                    o.Name,
                    o.City,
                    o.Country,
                    o.Phone,
                    Employees = o.Employees.Select(e => new
                    {
                        e.Id,
                        e.Name,
                        e.Email,
                        e.phone,
                        e.Salary
                    }
                 )
                })
                .ToList();
            return Ok(offices);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult getOfficeById(int id)
        {
            var office = _dbContext.Offices
                .Include(o => o.Employees)
                .Select(o => new
                {
                    o.Id,
                    o.Name,
                    o.City,
                    o.Country,
                    o.Phone,
                    Employees = o.Employees.Select(e => new
                    {
                        e.Id,
                        e.Name,
                        e.Email,
                        e.phone,
                        e.Salary
                    }
                 )
                })
                .FirstOrDefault(o => o.Id == id);

            if (office is null)
            {
                return NotFound();
            }
            return Ok(office);
        }

        [HttpPost]
        public IActionResult createOffice(AddOfficeDto office)
        {

            var newOffice = new Office()
            {
                Name = office.Name,
                City = office.City,
                Country = office.Country,
                Phone = office.Phone,
            };

            _dbContext.Offices.Add(newOffice);
            _dbContext.SaveChanges();

            return Ok(newOffice);

        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult updateOffice(int id, UpdateOfficeDto office)
        {
            var OfficeEntity = _dbContext.Offices.Find(id);
            if (OfficeEntity is null)
            {
                return NotFound();
            }

            OfficeEntity.Name = office.Name;
            OfficeEntity.City = office.City;
            OfficeEntity.Country = office.Country;
            OfficeEntity.Phone = office.Phone;

            _dbContext.Offices.Update(OfficeEntity);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult deleteOffice(int id)
        {
            var office = _dbContext.Offices.Find(id);
            if (office is null)
            {
                return NotFound();
            }

            _dbContext.Offices.Remove(office);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
