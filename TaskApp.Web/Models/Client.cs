using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApp.Web.Models
{
    public class Client
    {
        public int ClientId {get; set;}

        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [StringLength(25)]
        public string Address { get; set; }

        [ForeignKey("CompanyFK")]
        public int CompanyFK { get; set; }
        public Company Company { get; set; }
    }
}
