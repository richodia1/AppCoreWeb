using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Interfaces;
using TaskApp.Web.Models;

namespace TaskApp.Web.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateClientAsync(Client client)
        {
            if (client == null) return false;

            await _context.Set<Client>().AddAsync(client);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            var clients = await _context.Clients
                                 .Include(x => x.Company)
                                 .ToListAsync();

            return clients;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients
                                      .Include(c=>c.Company)
                                     .SingleOrDefaultAsync(x=>x.ClientId==id);
            return client;
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return client;
        }
    }
}
