using AutoMapper;
using CompaniesEx.Models;
using CompaniesEx.Models.Repositories.Companies;
using CompaniesEx.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompaniesEx.Controllers
{
    public class CompaniesController : Controller
    {
        private ICompaniesRepository _companiesRep;

        public CompaniesController(ICompaniesRepository companiesRep)
        {
            _companiesRep = companiesRep;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            IEnumerable<Company> companies;

            if (searchQuery != null)
            {
                companies = await _companiesRep.FindCompaniesByName(searchQuery);
            }
            else
            {
                companies = await _companiesRep.GetCompanies();
            }
            return View(companies);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEditCompanyViewModel model)
        {
            await _companiesRep.AddCompany(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id) {
            await _companiesRep.DeleteCompany(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id) {
            ViewBag.CompanyId = id;
            var company = await _companiesRep.GetCompanyById(id);
            return View(Mapper.Map<AddEditCompanyViewModel>(company));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddEditCompanyViewModel model)
        {
            await _companiesRep.EditCompany(model, id);
            return RedirectToAction("Index");
        }
    }
}
