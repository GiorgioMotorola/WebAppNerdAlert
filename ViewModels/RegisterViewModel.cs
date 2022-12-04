using System.ComponentModel.DataAnnotations;

namespace WebAppNerdAlert.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Email Address is Required")] 
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]        
        public  string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Does Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
