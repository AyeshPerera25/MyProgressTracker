using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models
{
    public class SiginInViewModel
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Contact No")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Birth Day")]
        [DataType(DataType.Date)]
        public DateOnly BirthDay { get; set; }

        [Display(Name = "Institute")]
        public string Institute { get; set; }

        [Display(Name = "Course")]
        public string Course { get; set; }

        [Display(Name = "Stage")]
        public string Stage { get; set; }
    }
}
