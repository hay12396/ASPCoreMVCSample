using CompaniesEx.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompaniesEx.ViewModels;
using AutoMapper;
using System;
using System.Linq;

namespace CompaniesEx.Models.Repositories.Companies
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly AppDbContext _context;

        public CompaniesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCompany(AddEditCompanyViewModel model)
        {
            var company = Mapper.Map<Company>(model);

            _context.Companies.Add(company);

            await _context.SaveChangesAsync();
        }

        public async Task EditCompany(AddEditCompanyViewModel model, int id)
        {
            var eCompany = await GetCompanyById(id);
            if (eCompany == null) return;

            eCompany.PHC = model.PHC;
            eCompany.Name = model.Name;
            eCompany.Address = model.Address;
            eCompany.PhoneNumber = model.PhoneNumber;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int id)
        {
            var companyToDelete = await GetCompanyById(id);
            if (companyToDelete == null) return;

            _context.Companies.Remove(companyToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies.Include(c => c.Employees).ToListAsync();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _context.Companies.Include(c=>c.Employees).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Company>> FindCompaniesByName(string name)
        {
            return await _context.Companies.Where(c => c.Name.Contains(name)).ToListAsync();
        }
    }
}
