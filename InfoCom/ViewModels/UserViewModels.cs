using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Models;

namespace InfoCom.ViewModels
{
    public class UserRegisterViewModel
    {

        [RegularExpression("^[a-zA-ZåäöÅÄÖ0-9\\-.]+$", ErrorMessage = "Username can only consist of letters, dashes and dots.")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 - 30 characters long.")]
        //[Remote("ValidateUsername", "User", HttpMethod = "POST", ErrorMessage = "Username already exists. Please enter a different username.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        //[Remote("ValidateEmail", "User", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [RegularExpression("^([0-9]+[a-zA-Z]+|[a-zA-Z]+[0-9]+)[0-9a-zA-Z]*$", ErrorMessage = "Password must at least contain one letter and one number, no other characters allowed.")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6-50 characters.")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password does not match the confirm password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }

    public class UserIndexViewmodel
    {
        public ICollection<User> Users { get; set; }
    }

    public class UserEditViewModel
    {
        public int Id { get; set; }
        [RegularExpression("^[a-zA-ZåäöÅÄÖ0-9\\-.]+$", ErrorMessage = "Username can only consist of letters, dashes and dots.")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 - 30 characters long.")]
        //[Remote("ValidateUsername", "User", HttpMethod = "POST", ErrorMessage = "Username already exists. Please enter a different username.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        //[Remote("ValidateEmail", "User", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}