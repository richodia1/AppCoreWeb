using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Models;

namespace TaskApp.Web.ViewModels
{
    public class ClientCreateViewModel
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [StringLength(25)]
        public string Address { get; set; }

        [Display(Name = "Company")]
        public int SelectedCompanyId { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
    }
}
