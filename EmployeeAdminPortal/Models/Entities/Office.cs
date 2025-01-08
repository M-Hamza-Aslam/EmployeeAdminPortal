namespace EmployeeAdminPortal.Models.Entities
{
    public class Office
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public string? Phone { get; set; }

        // Collection navigation containing dependents
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
