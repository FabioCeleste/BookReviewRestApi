using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookReview.Domain;

namespace BookReview.Application.Dtos
{
    public class BookDto
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "the title field is necessary")]
        public string Title {get; set;}

        [Required(ErrorMessage = "the author field is necessary")]
        public string Author { get; set; }

        //TO-DO
        // The book score will be the average score of users reviews of the book 
        // [Required(ErrorMessage = "the score field is necessary")]
        // [Range(0, 10, ErrorMessage ="the score must been a number between 0 and 10")]
        public int Score {get; set;}
        
        public ICollection<ReviewDto> Reviews { get; set; }
    }
}