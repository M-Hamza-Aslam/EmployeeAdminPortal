namespace EmployeeAdminPortal.Models
{
    public class AddOfficeDto
    {
        public required string Name { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public string? Phone { get; set; }
    }
}
