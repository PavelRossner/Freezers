namespace Mrazaky.Models
{
    public class DashboardViewModel
    {
        public int DashboardViewModelId { get; set; }
        public int FoodCount { get; set; }
        public int FreezerCount { get; set; }
        public int FoodCategoryCount { get; set; }
        public int FreezerFoodCount { get; set; }
        public int ExpiredFood { get; set; }
        public int NonExpiredFood { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
