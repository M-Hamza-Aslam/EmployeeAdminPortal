namespace EmployeeAdminPortal.Dtos.Employee
{
    public class GetAllEmployeesReqDto
    {
        public string? searchText;
        public string? sortField = "Name";
        public string? sortOrder = "asc";
        public int pageNumber = 1;
        public int pageSize = 10;
    }
}
