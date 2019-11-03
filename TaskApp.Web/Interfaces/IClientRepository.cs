using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Models;

namespace TaskApp.Web.Interfaces
{
    public interface IClientRepository
    {
        Task<bool> CreateClientAsync(Client client);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
    }
}
