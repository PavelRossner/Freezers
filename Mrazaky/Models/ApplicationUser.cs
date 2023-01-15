using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Mrazaky.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        //[Display(Name = "Id")]

        [Display(Name = "Email")]
        public override string Email { get; set; } = "";

        [Display(Name = "Jméno")]
        public string FirstName { get; set; } = "";

        [Display(Name = "Příjmení")]
        public string LastName { get; set; } = "";

        public virtual ICollection<Food> Foods { get; set; } = new HashSet<Food>();
        public virtual ICollection<Freezer> Freezers { get; set; } = new HashSet<Freezer>();
    }
}
