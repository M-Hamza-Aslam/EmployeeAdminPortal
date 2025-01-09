using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Dtos.Office;
using EmployeeAdminPortal.DTOs.Office;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Services
{
    public class OfficeService
    {
        private readonly ApplicationDbContext _dbContext;

        public OfficeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<OfficeResponseDto> GetAll()
        {
            var offices = _dbContext.Offices
            .Include(o => o.Employees)
            .Select(o => new OfficeResponseDto
            {
                Id = o.Id,
                Name = o.Name,
                City = o.City,
                Country = o.Country,
                Phone = o.Phone,
                Employees = o.Employees.Select(e => new OfficeEmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    phone = e.Phone,
                    Salary = e.Salary
                }).ToList()
            }).ToList();

            return offices;
        }

        public OfficeResponseDto? GetById(int id)
        {
            var office = _dbContext.Offices
                .Include(o => o.Employees)
                .Select(o => new OfficeResponseDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    City = o.City,
                    Country = o.Country,
                    Phone = o.Phone,
                    Employees = o.Employees.Select(e => new OfficeEmployeeDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Email = e.Email,
                        phone = e.Phone,
                        Salary = e.Salary
                    }).ToList()
                })
                .FirstOrDefault(o => o.Id == id);

            return office;

        }

        public int? Create(AddOfficeDto office)
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

            return newOffice.Id;
        }

        public int? Update(int id, UpdateOfficeDto office)
        {
            var OfficeEntity = _dbContext.Offices.Find(id);
            if (OfficeEntity is null)
            {
                return null;
            }

            OfficeEntity.Name = office.Name;
            OfficeEntity.City = office.City;
            OfficeEntity.Country = office.Country;
            OfficeEntity.Phone = office.Phone;

            _dbContext.Offices.Update(OfficeEntity);
            _dbContext.SaveChanges();

            return OfficeEntity.Id;
        }

        public int? Delete(int id)
        {
            var office = _dbContext.Offices.Find(id);
            if (office is null)
            {
                return null;
            }

            _dbContext.Offices.Remove(office);
            _dbContext.SaveChanges();

            return office.Id;
        }
    }
}
