using System.ComponentModel.DataAnnotations;

namespace InfoCom.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter a username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter a password")]
        public string Password { get; set; }

    }
}