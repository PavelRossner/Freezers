namespace Mrazaky.Models
{
    /// <summary>
    /// Holds information about user's freezers
    /// </summary>
    public partial class ApplicationUserFreezer
    {
        /// <summary>
        /// Gets or sets the user freezer identifier.
        /// </summary>
        public int ApplicationUserFreezerID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string ApplicationUserID { get; set; }

        /// <summary>
        /// Gets or sets the freezed identifier.
        /// </summary>
        public int FreezerID { get; set; }

        /// <summary>
        /// Gets or sets the create at. => This was not used in the final version.
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active. => This was not used in the final version.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the application user.
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or sets the freezer.
        /// </summary>
        public virtual Freezer Freezer { get; set; }

        
    }
}