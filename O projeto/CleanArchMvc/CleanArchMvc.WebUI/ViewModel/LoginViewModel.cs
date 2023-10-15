using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.WebUI.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid format email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20,MinimumLength = 3,ErrorMessage = "Password have to has max{1} caracters and minimum {2} caracters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
