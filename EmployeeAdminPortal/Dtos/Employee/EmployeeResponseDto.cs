namespace EmployeeAdminPortal.Dtos.Employee
{
    public class EmployeeOfficeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public string? Phone { get; set; }
    }
    public class EmployeeProjectDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }
    }
    public class EmployeeResponseDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? Phone { get; set; }

        public required decimal Salary { get; set; }

        public EmployeeOfficeDto? Office { get; set; }

        public List<EmployeeProjectDto>? Projects { get; set; }

    }
}
