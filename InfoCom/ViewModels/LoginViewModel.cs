using System.ComponentModel.DataAnnotations;

namespace InfoCom.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter a username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}