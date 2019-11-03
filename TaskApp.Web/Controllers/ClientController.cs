using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskApp.Web.Interfaces;
using TaskApp.Web.Models;
using TaskApp.Web.ViewModels;

namespace TaskApp.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ICompanyService _companyService;
        public ClientController(IClientService clientService, ICompanyService companyService)
        {
            _clientService = clientService;
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            var clientViewModel = new ClientViewModel
            {
                clients = await _clientService.GetAllClientAsync()
            };

            return View(clientViewModel);
        }

        public async Task<IActionResult> Create(ClientCreateViewModel clientCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new Client
                    {
                        Name = clientCreateViewModel.Name,
                        CompanyFK = clientCreateViewModel.SelectedCompanyId,
                        Address = clientCreateViewModel.Address
                    };

                    var result = await _clientService.CreateClientAsync(client);
                    if (result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes." + ex.Message);
            }

            clientCreateViewModel.Companies = await GetCompanySelections();

            return View(clientCreateViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = await _clientService.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetClientByIdAsync(id);

            var viewModel = new ClientCreateViewModel
            {
                ClientId = client.ClientId,
                Name = client.Name,
                Address = client.Address,
                SelectedCompanyId = client.CompanyFK,
                Companies = await GetCompanySelections()
            };

            if (client == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ClientCreateViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _clientService.UpdateClientAsync(clientViewModel);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes." + ex.Message);
                }
            }

            return View(clientViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clientService.DeleteClientAsync(id);
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Delete", new { id = id });
            }

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetCompanySelections()
        {
            var companies = await _companyService.GetCompaniesAsync();
            var selectedItems = companies.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ID.ToString(),
                Selected = false
            });
            return selectedItems;
        }
    }
}