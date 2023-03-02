using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mrazaky.Models
{
    /// <summary>
    /// Freezer and food junction table
    /// </summary>
    public partial class FreezerFood
    {
        /// <summary>
        /// Gets or sets the freezer food identifier.
        /// </summary>
        public int FreezerFoodID { get; set; }

        /// <summary>
        /// Gets or sets the food identifier.
        /// </summary>
        public int FoodID { get; set; }

        /// <summary>
        /// Gets or sets the freezer identifier.
        /// </summary>
        public int FreezerID { get; set; }

        /// <summary>
        /// Gets or sets the freezer position.
        /// </summary>
        public string FreezerName { get; set; }

        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the food.
        /// </summary>
        public virtual Food Food { get; set; }

        /// <summary>
        /// Gets or sets the freezer.
        /// </summary>
        public virtual Freezer Freezer { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}