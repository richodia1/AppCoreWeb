using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Models;

namespace TaskApp.Web.ViewModels
{
    public class CompanyViewModel
    {
        public IEnumerable<Company> CompanyList { get; set; }
    }
}
