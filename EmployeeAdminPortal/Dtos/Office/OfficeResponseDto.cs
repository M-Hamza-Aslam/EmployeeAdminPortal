namespace EmployeeAdminPortal.Dtos.Office
{
    public class OfficeEmployeeDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? phone { get; set; }

        public required decimal Salary { get; set; }
    }
    public class OfficeResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public string? Phone { get; set; }
        public List<OfficeEmployeeDto>? Employees { get; set; }

    }
}
