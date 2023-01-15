using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mrazaky.Models.Response
{
    public class FoodResponse : Food
    {

        public int daysToConsume;

        [Display(Name = "Dny do spotřeby")]
        public int DaysToConsume
        {
            get { return (int)(BestBefore - DateTime.Today).TotalDays; }
            set { daysToConsume = value; }
        }

        public static FoodResponse GetFoodResponse(Food food)
        {
            return new FoodResponse

            {
                FoodId = food.FoodId,
                Category = food.Category,
                FoodName = food.FoodName,
                NumberOfPackages = food.NumberOfPackages,
                DatePurchase = food.DatePurchase,
                BestBefore = food.BestBefore,
                Freezer = food.Freezer,
                FreezerPosition = food.FreezerPosition,
                PackageLabel = food.PackageLabel,
            };

        }


    }
}
