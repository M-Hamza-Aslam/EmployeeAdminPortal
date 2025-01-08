using EmployeeAdminPortal.Data;
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

        public List<Office> GetAllOfficesService()
        {
            var offices = _dbContext.Offices
                                    .Include(o => o.Employees)
                                    .Select(o => new Office
                                    {
                                        Id = o.Id,
                                        Name = o.Name,
                                        City = o.City,
                                        Country = o.Country,
                                        Phone = o.Phone,
                                        Employees = o.Employees.Select(e => new Employee
                                        {
                                            Id = e.Id,
                                            Name = e.Name,
                                            Email = e.Email,
                                            phone = e.phone,
                                            Salary = e.Salary
                                        }).ToList()
                                    }).ToList();

            return offices;
        }
    }
}
