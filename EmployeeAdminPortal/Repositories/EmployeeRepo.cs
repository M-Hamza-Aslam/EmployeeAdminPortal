using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeAdminPortal.Repositories
{
    public class EmployeeRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeRepo(ApplicationDbContext dbContext)
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

        public IQueryable<Employee> GetAll(
             string? searchText,
            string? sortField,
            string? sortOrder)
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

            return allEmployeesQuery;
        }

        public Employee? GetById(Guid id)
        {
            return _dbContext.Employees
                .Include(e => e.Office)
                .Include(e => e.Projects)
                .FirstOrDefault();
        }

        public Guid Create(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            return employee.Id;
        }

        public Guid Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();

            return employee.Id;
        }

        public Guid Remove(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();

            return employee.Id;
        }

    }
}
