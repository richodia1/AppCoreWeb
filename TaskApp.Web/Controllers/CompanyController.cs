using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApp.Web.Interfaces;
using TaskApp.Web.Models;
using TaskApp.Web.ViewModels;

namespace TaskApp.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new CompanyViewModel
            {
                CompanyList = await _service.GetCompaniesAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create(Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _service.CreateCompanyAsync(company);
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

            return View(company);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var company = await _service.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _service.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateCompanyAsync(company);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes." + ex.Message);
                }
            }

            return View(company);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _service.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {  
            try
            {
                await _service.DeleteCompanyAsync(id);
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Delete", new { id = id});
            }

            return RedirectToAction("Index");
        }
    }
}