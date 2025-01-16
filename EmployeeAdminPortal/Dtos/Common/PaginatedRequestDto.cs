namespace EmployeeAdminPortal.Dtos.Common
{
    public class PaginatedRequestDto
    {
        public string? SearchText { get; set; }
        public string? SortField { get; set; } = "Name";
        public string? SortOrder { get; set; } = "asc";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
