namespace EmployeeAdminPortal.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? Phone { get; set; }

        public required decimal Salary { get; set; }
        public required int OfficeId { get; set; }

    }
}
