namespace EmployeeAdminPortal.DTOs.Office
{
    public class UpdateOfficeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public string? Phone { get; set; }
    }
}
