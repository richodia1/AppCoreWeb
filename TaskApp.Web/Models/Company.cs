using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApp.Web.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [StringLength(25)]
        public string Address { get; set; }

        [ForeignKey("CompanyFK")]
        public ICollection<Client> Clients { get; set; }

        public Company()
        {
            Clients = new HashSet<Client>();
        }
    }
}
