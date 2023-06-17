using System.ComponentModel.DataAnnotations;

namespace Chat_application.Dtos
{
    public class UserDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name not valide! add more characters..")]
        public string Name { get; set; }
    }
}
