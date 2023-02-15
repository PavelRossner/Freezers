using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mrazaky.Models
{
    public partial class Food
    {
        public int FoodId { get; set; }

        [Display(Name = "Kategorie")]
        public string Category { get; set; }

        [Display(Name = "Název potraviny")]
        public string FoodName { get; set; }

        [Display(Name = "Název mrazáku")]
        public string FreezerName { get; set; }

        [Display(Name = "Počet balení")]
        public string NumberOfPackages { get; set; }

        [Display(Name = "Hmotnost (kg)")]
        public string Weight { get; set; }

        [Display(Name = "Datum zakoupení/uložení")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DatePurchase { get; set; }

        [Display(Name = "Spotřeba na obalu")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime BestBefore { get; set; }

        [Display(Name = "Označení balíčku")]
        public string PackageLabel { get; set; }

        [Display(Name = "Pozice v mrazáku")]
        public string FreezerPosition { get; set; }

        [Display(Name = "Poznámka")]
        public string Note { get; set; } = "";

        /// <summary>
        /// Gets or sets the freezer food.
        /// </summary>
        public virtual ICollection<FreezerFood> FreezerFood { get; set; } = new HashSet<FreezerFood>();
    }
}
