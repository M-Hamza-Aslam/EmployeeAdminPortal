﻿namespace EmployeeAdminPortal.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? phone { get; set; }

        public required decimal Salary { get; set; }

        // Foreign Key to Office
        public int OfficeId { get; set; }

        // Navigation Property
        public Office Office { get; set; } = null!;

    }
}
