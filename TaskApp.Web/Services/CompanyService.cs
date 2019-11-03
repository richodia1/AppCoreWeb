using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Interfaces;
using TaskApp.Web.Models;

namespace TaskApp.Web.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;
        public CompanyService(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            if (company == null) throw new InvalidOperationException("Invalid Company data");

            return await _repo.CreateCompanyAsync(company);
        }

        public async Task DeleteCompanyAsync(int? id)
        {
            if (id != null)
            {
                await _repo.DeleteCompanyAsync(id.Value);
            }
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _repo.GetCompaniesAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int? id)
        {
            var company = await _repo.GetCompanyByIdAsync(id.Value);

            return company;
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            var comp = await _repo.GetCompanyByIdAsync(company.ID);

            if (comp != null)
            {
                comp.Name = company.Name;
                comp.Address = company.Address;
            }

            return await _repo.UpdateCompanyAsync(comp);
        }
    }
}
