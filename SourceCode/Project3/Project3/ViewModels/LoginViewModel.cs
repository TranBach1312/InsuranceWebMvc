using System.ComponentModel.DataAnnotations;

namespace Project3.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public Boolean RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
