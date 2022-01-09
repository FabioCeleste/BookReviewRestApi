using System.ComponentModel.DataAnnotations;
using BookReview.Domain;

namespace BookReview.Application.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "the content field is necessary")]
        public string Content { get; set; }

        [Required(ErrorMessage = "the book field is necessary")]
        public Book Book { get; set; }

        [Required(ErrorMessage = "the score field is necessary")]
        [Range(0, 10, ErrorMessage ="the score must been a number between 0 and 10")]
        public int Score { get; set; }
    }
}