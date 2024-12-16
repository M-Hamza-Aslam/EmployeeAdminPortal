﻿namespace EmployeeAdminPortal.Models
{
    public class AddEmployeeDto
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? phone { get; set; }

        public required decimal Salary { get; set; }
    }
}