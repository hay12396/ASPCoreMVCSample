using CompaniesEx.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompaniesEx.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CompaniesEx.Models.Repositories.Employees
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly AppDbContext _context;
        public EmployeesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployee(AddEditEmployeeViewModel model, int companyId)
        {
            var newEmployee = Mapper.Map<Employee>(model);
            newEmployee.CompanyId = companyId;

            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employeeToDelete = await GetEmployeeById(id);
            if (employeeToDelete == null) return;

            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task EditEmployee(AddEditEmployeeViewModel model, int employeeId)
        {
            var employee = await GetEmployeeById(employeeId);
            if (employee == null) return;

            employee.Birthday = model.Birthday;
            employee.FirstName = model.FirstName;
            employee.IdNumber = model.IdNumber;
            employee.LastName = model.LastName;
            employee.Sex = model.Sex;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> FindEmployeeByName(string name)
        {
            return await _context.Employees.Where(e => e.FirstName.Contains(name)).ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }
    }
}
