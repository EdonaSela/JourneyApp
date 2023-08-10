using System.ComponentModel.DataAnnotations;

namespace JourneyApp.Models.DTOs.Register
{
    public class RegisterDto
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
