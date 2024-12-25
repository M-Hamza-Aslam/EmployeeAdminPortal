using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.DTOs.Employee;
using EmployeeAdminPortal.DTOs.Office;
using EmployeeAdminPortal.DTOs.Project;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ProjectController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private static Expression<Func<Project, object>> GetSortExpression(string? sortField)
        {
            return sortField?.ToLower() switch
            {
                "description" => e => e.Description,
                _ => e => e.Name // Default to Name
            };
        }

        [HttpGet("find")]
        public IActionResult GetAllProjects(
            string? searchText,
            string? sortField = "Name",
            string? sortOrder = "asc",
            int pageNumber = 1,
            int pageSize = 10
       )
        {
            var allProjectsQuery = _dbContext.Projects
                .Include(p => p.Employees).AsQueryable();

            //searching
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                allProjectsQuery = allProjectsQuery.Where(p => p.Name.Contains(searchText) || p.Description.Contains(searchText));
            }

            // Sorting
            allProjectsQuery = sortOrder?.ToLower() == "desc"
            ? allProjectsQuery.OrderByDescending(GetSortExpression(sortField))
                : allProjectsQuery.OrderBy(GetSortExpression(sortField));

            // Paging
            var totalRecords = allProjectsQuery.Count();

            var projects = allProjectsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    Employees = p.Employees.Select(e => new
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

            return Ok(new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = projects
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _dbContext.Projects
                .Include(p => p.Employees)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    Employees = p.Employees.Select(e => new
                        {
                            e.Id,
                            e.Name,
                            e.Email,
                            e.phone,
                            e.Salary
                        }
                    )
                })
                .FirstOrDefault(p => p.Id == id);

            if (project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public IActionResult createProject(AddProjectDto project)
        {

            var newProject = new Project()
            {
                Name = project.Name,
                Description = project.Description,
            };

            _dbContext.Projects.Add(newProject);
            _dbContext.SaveChanges();

            return Ok(newProject);

        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateProject(int id, UpdateProjectDto updateProjectDto)
        {
            var project = _dbContext.Projects.Find(id);
            if (project is null)
            {
                return NotFound();
            }

            project.Name = updateProjectDto.Name;
            project.Description = updateProjectDto.Description;

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return Ok(project);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _dbContext.Projects.Find(id);

            if (project is null)
            {
                return NotFound();
            }

            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
