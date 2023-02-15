using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Mrazaky.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        /// <summary>
        /// Gets or sets the user's freezer.
        /// </summary>
        public virtual ICollection<ApplicationUserFreezer> UserFreezer { get; set; } = new HashSet<ApplicationUserFreezer>();

        // TODO - delete => not deleted - it was needed for the application to work properly
        public virtual ICollection<Food> Foods { get; set; } = new HashSet<Food>();

        // TODO - delete => not deleted - it was needed for the application to work properly
        public virtual ICollection<Freezer> Freezers { get; set; } = new HashSet<Freezer>();

    }
}
