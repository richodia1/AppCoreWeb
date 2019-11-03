using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Interfaces;
using TaskApp.Web.Models;
using TaskApp.Web.ViewModels;

namespace TaskApp.Web.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repo;
        public ClientService(IClientRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateClientAsync(Client client)
        {
            if (client == null) throw new InvalidOperationException("Invalid client data");

            return await _repo.CreateClientAsync(client);
        }

        public async Task DeleteClientAsync(int? id)
        {
            if (id != null)
            {
                await _repo.DeleteClientAsync(id.Value);
            }
        }

        public async Task<IEnumerable<Client>> GetAllClientAsync()
        {
            return await _repo.GetClientsAsync();
        }

        public async Task<Client> GetClientByIdAsync(int? id)
        {
            var client = await _repo.GetClientByIdAsync(id.Value);

            return client;
        }

        public async Task<Client> UpdateClientAsync(ClientCreateViewModel clientViewModel)
        {
            var client = await _repo.GetClientByIdAsync(clientViewModel.ClientId);

            if (client != null)
            {
                client.Name = clientViewModel.Name;
                client.Address = clientViewModel.Address;
                client.CompanyFK = clientViewModel.SelectedCompanyId;
            }

            return await _repo.UpdateClientAsync(client);
        }
    }
}
