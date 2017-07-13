using CompaniesEx.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompaniesEx.Models.Repositories.Companies
{
    public interface ICompaniesRepository
    {
        Task<Company> GetCompanyById(int id);
        Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<Company>> FindCompaniesByName(string name);

        Task EditCompany(AddEditCompanyViewModel model, int id);
        Task AddCompany(AddEditCompanyViewModel model);
        Task DeleteCompany(int id);
    }
}
