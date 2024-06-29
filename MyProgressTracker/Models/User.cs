using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }   
        public DateOnly BirthDay { get; set; }
        public string Institute { get; set; }
        public string Course { get; set; }
        public string EducationLvl { get; set; }
    }
}
