using AutoMapper;
using CompaniesEx.Models;
using CompaniesEx.Models.Repositories.Companies;
using CompaniesEx.Models.Repositories.Employees;
using CompaniesEx.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompaniesEx.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeesRepository _employeesRep;
        private ICompaniesRepository _companiesRep;

        public EmployeesController(IEmployeesRepository employeesRep, ICompaniesRepository companiesRep)
        {
            _employeesRep = employeesRep;
            _companiesRep = companiesRep;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            IEnumerable<Employee> employees;
            if (searchQuery != null) {
                employees = await _employeesRep.FindEmployeeByName(searchQuery);
            }
            else {
                employees = await _employeesRep.GetEmployees();
            }
            
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add(int companyId)
        {
            ViewBag.CompanyId = companyId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEditEmployeeViewModel model, int companyId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CompanyId = companyId;
                ViewBag.IdNumberError = ModelState[nameof(model.IdNumber)]?.Errors[0].ErrorMessage;
                
                return View(model);
            }

            if (await _companiesRep.GetCompanyById(companyId) != null)
            {
                await _employeesRep.AddEmployee(model, companyId);
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _employeesRep.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeesRep.GetEmployeeById(id);
            return View(Mapper.Map<AddEditEmployeeViewModel>(employee));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddEditEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdNumberError = ModelState[nameof(model.IdNumber)]?.Errors[0].ErrorMessage;

                return View(model);
            }

            await _employeesRep.EditEmployee(model, id);
            return RedirectToAction("Index");
        }
    }
}
