using System;
using System.ComponentModel.DataAnnotations;

namespace Allianz.Vita.Quality.Models
{
    public class SignInViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}