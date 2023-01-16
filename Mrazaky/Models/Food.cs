using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mrazaky.Models
{
    public class Food
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int FoodId { get; set; }
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Kategorie")]
        public string Category { get; set; } = "";

        [Display(Name = "Název potraviny")]
        public string FoodName { get; set; } = "";

        [Display(Name = "Počet balení")]
        public int NumberOfPackages { get; set; }

        [Display(Name = "Datum zakoupení")]
        public DateTime DatePurchase { get; set; }

        [Display(Name = "Datum spotřeby")]
        public DateTime BestBefore { get; set; }

        [Display(Name = "Mrazák")]
        public string Freezer { get; set; } = "";

        [Display(Name = "Pozice v mrazáku")]
        public string FreezerPosition { get; set; } = "";

        [Display(Name = "Označení balíčku")]
        public string PackageLabel { get; set; } = "";

        //Navigation property
        public virtual ICollection<FoodFreezer> FoodFreezers { get; set; }

    }
}
