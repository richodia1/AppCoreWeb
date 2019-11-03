using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Interfaces;
using TaskApp.Web.Models;

namespace TaskApp.Web.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            if (company == null) return false;

            await _context.Set<Company>().AddAsync(company);

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return await Task.FromResult<bool>(true);
            }
            else
            {
                return await Task.FromResult<bool>(false);
            }
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            _context.Companies.Remove(company);

            await _context.SaveChangesAsync();
        }

        public async Task<Company[]> GetCompaniesAsync()
        {
            var companies = await _context.Companies
                                          .ToListAsync();

            return companies.ToArray();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            var company = await _context.Companies
                                       .Include(x => x.Clients)
                                       .SingleOrDefaultAsync(i => i.ID == id);


            return company;
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return company;
        }
    }
}
