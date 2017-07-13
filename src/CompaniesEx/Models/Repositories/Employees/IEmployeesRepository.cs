using CompaniesEx.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompaniesEx.Models.Repositories.Employees
{
    public interface IEmployeesRepository
    {
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<IEnumerable<Employee>> FindEmployeeByName(string name);

        Task EditEmployee(AddEditEmployeeViewModel model, int employeeId);
        Task AddEmployee(AddEditEmployeeViewModel model, int companyId);
        Task DeleteEmployee(int id);
    }
}
