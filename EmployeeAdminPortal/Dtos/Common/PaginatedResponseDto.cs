namespace EmployeeAdminPortal.Dtos.Common
{
    public class PaginatedResponseDto<T>
    {
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public required List<T> Data { get; set; }
    }
}
