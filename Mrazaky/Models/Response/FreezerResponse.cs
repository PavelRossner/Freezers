using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mrazaky.Models.Response
{
    public class FreezerResponse : Freezer
    {
        public DateTime nextDefrosted;

        [Display(Name = "Příští odmražení")]
        public DateTime NextDefrosted
        {
            get { return (LastDefrosted.AddDays(DefrostInterval)); }
            set { nextDefrosted = value; }
        }

        public int daysToDefrost;

        [Display(Name = "Dny do odmražení")]
        public int DaysToDefrost
        {
            get { return (int)(NextDefrosted - DateTime.Today).TotalDays; }

            set { daysToDefrost = value; }
        }

        public static FreezerResponse GetFreezerResponse(Freezer freezer)
        {
            return new FreezerResponse
            {
                FreezerId = freezer.FreezerId,
                Order= freezer.Order,
                FreezerLocation= freezer.FreezerLocation,
                NumberOfShelves= freezer.NumberOfShelves,
                LastDefrosted= freezer.LastDefrosted,
                DefrostInterval= freezer.DefrostInterval,
                Note= freezer.Note,
            };
        }

    }
}
