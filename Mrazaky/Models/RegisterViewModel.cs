using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Mrazaky.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "Délka {0} musí být alespoň {2} znaky a nejvíce {1} znaků.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Potvrď heslo")]
        [Compare("Password", ErrorMessage = "Hesla musí být stejná.")]
        public string ConfirmPassword { get; set; } = "";


        [Display(Name = "Jméno")]
        public string FirstName { get; set; } = "";

        [Display(Name = "Příjmení")]
        public string LastName { get; set; } = "";
    }
}
