namespace EmployeeAdminPortal.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; } = new List<Employee>();
    }
}
