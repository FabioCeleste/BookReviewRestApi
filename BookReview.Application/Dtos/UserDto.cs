using System.ComponentModel.DataAnnotations;

namespace BookReview.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "the username field is necessary")]
        [RegularExpression(@"^[ a-zA-Z á]*$", ErrorMessage = "the username can only have letters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "the email field is necessary")]
        [EmailAddress(ErrorMessage = "invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "the password field is necessary")]
        public string Password { get; set; }
    }
}