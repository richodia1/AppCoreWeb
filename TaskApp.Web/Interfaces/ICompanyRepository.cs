using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Models;

namespace TaskApp.Web.Interfaces
{
    public interface ICompanyRepository
    {
        Task<bool> CreateCompanyAsync(Company company);
        Task<Company[]> GetCompaniesAsync();
        Task<Company> GetCompanyByIdAsync(int id);
        Task<Company> UpdateCompanyAsync(Company company);
        Task DeleteCompanyAsync(int id);
    }
}
