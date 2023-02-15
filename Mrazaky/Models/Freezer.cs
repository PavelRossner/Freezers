using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mrazaky.Models
{
    public partial class Freezer
    {
        public int FreezerId { get; set; }

        [Display(Name = "Název mrazáku")] // TODO
        public string FreezerName { get; set; } = "";

        [Display(Name = "Počet přihrádek")]// TODO
        public int NumberOfShelves { get; set; }

        [Display(Name = "Poslední odmražení")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]// TODO
        public DateTime LastDefrosted { get; set; }

        [Display(Name = "Interval odmražení (dny)")]// TODO
        public int DefrostInterval { get; set; }

        [Display(Name = "Poznámka")]// TODO
        public string Note { get; set; } = "";

        /// <summary>
        /// Gets or sets the user freezer.
        /// </summary>
        public virtual ICollection<ApplicationUserFreezer> UserFreezer { get; set; } = new HashSet<ApplicationUserFreezer>();

        /// <summary>
        /// Gets or sets the freezer food.
        /// </summary>
        public virtual ICollection<FreezerFood> FreezerFood { get; set; } = new HashSet<FreezerFood>();

        //TODO - delete Navigation property => not deleted - needed in FoodsController
        public virtual ICollection<Food> Foods { get; set; } = new HashSet<Food>();
    }
}
