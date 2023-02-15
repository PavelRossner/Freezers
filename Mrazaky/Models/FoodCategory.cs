using System.ComponentModel.DataAnnotations;

namespace Mrazaky.Models
{
    public class FoodCategory
    {
        public int FoodCategoryId { get; set; }

        [Display(Name = "Kategorie potraviny")]
        public string FoodCategoryName { get; set; }
    }
}
