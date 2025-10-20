using System.ComponentModel.DataAnnotations;

namespace FlyingProject.Project.core.DTOS.ViewModles
{
    public class LoginViewModle
    {
        [Required(ErrorMessage ="Emial is Requird")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Requird")]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
