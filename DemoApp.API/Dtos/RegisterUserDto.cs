using System.ComponentModel.DataAnnotations;

namespace DemoApp.API.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="Password length not matching criteria")]
        public string Password { get; set; }
    }
}