using System.ComponentModel.DataAnnotations;

namespace NSE.Identidade.API.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "The field {0} is in invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The passwords doesn't match")]
        public string PasswordConfirmation { get; set; }
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "The field {0} is in invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }

}
