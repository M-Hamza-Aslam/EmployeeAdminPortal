using EmployeeAdminPortal.Data;

namespace EmployeeAdminPortal.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetAllEmployeesService()
        {
            return 1;
        }

    }
}
