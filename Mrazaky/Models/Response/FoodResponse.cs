using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;

namespace Mrazaky.Models.Response
{
    public class FoodResponse : Food
    {
        public DateTime consumption;

        [Display(Name = "Spotřeba (měsíc/rok)")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM.yyyy}")]
        public DateTime Consumption

        {
            get { return (DatePurchase.AddMonths(15)); }
            set { consumption = value; }
        }

        public int monthsToConsume;

        [Display(Name = "Spotřebovat za (měsíce)")]
        public int MonthsToConsume
        {
            get { return (int)(((DatePurchase.Year + 1) - DateTime.Today.Year) * 12) + ((DatePurchase.Month + 3) - DateTime.Today.Month); }
            set { monthsToConsume = value; }
        }

        public static FoodResponse GetFoodResponse(Food food)
        {
            return new FoodResponse

            {
                FoodId = food.FoodId,
                Category = food.Category,
                FoodName = food.FoodName,
                FreezerName = food.FreezerName,
                NumberOfPackages = food.NumberOfPackages,
                Weight = food.Weight,
                PackageLabel = food.PackageLabel,
                FreezerPosition = food.FreezerPosition,
                DatePurchase = food.DatePurchase,
                BestBefore = food.BestBefore,
                Note = food.Note,
            };

        }
    }
}
