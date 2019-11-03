using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Models;
using TaskApp.Web.ViewModels;

namespace TaskApp.Web.Interfaces
{
    public interface IClientService
    {
        Task<bool> CreateClientAsync(Client client);
        Task<IEnumerable<Client>> GetAllClientAsync();
        Task<Client> GetClientByIdAsync(int? id);
        Task<Client> UpdateClientAsync(ClientCreateViewModel client);
        Task DeleteClientAsync(int? id);
    }
}
