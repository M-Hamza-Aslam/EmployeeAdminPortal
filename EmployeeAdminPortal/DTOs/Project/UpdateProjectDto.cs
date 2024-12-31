namespace EmployeeAdminPortal.DTOs.Project
{
    public class UpdateProjectDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public List<Guid>? EmployeeIds { get; set; }
    }
}
