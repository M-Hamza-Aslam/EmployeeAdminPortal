namespace EmployeeAdminPortal.DTOs.Project
{
    public class AddProjectDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public List<Guid>? EmployeeIds { get; set; }
    }
}
