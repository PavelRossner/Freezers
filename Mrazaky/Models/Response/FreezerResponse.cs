using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Mrazaky.Models.Response
{
    public class FreezerResponse : Freezer
    {
        public DateTime nextDefrosted;

        [Display(Name = "Příští odmražení")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
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
                FreezerName= freezer.FreezerName,
                NumberOfShelves= freezer.NumberOfShelves,
                LastDefrosted= freezer.LastDefrosted,
                DefrostInterval= freezer.DefrostInterval,
                Note= freezer.Note,
            };
        }

    }
}
