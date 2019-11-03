using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Web.Models;

namespace TaskApp.Web.ViewModels
{
    public class ClientViewModel
    {
        public IEnumerable<Client> clients { get; set; }
    }
}
