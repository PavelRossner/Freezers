using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mrazaky.Models
{
    public class Freezer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int FreezerId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Pořadové číslo")]
        public int Order { get; set; }

        [Display(Name = "Mrazák")]
        public string FreezerLocation { get; set; } = "";

        [Display(Name = "Počet přihrádek")]
        public int NumberOfShelves { get; set; }

        [Display(Name = "Poslední odmražení")]
        public DateTime LastDefrosted { get; set; }

        [Display(Name = "Interval odmražení (dny)")]
        public int DefrostInterval { get; set; }        

        [Display(Name = "Poznámka")]
        public string Note { get; set; } = "";

        //Navigation property
        public virtual ICollection<Food> Foods { get; set; } = new HashSet<Food>();
    }
}
