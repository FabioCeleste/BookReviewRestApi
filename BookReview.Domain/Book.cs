using System.Collections.Generic;

namespace BookReview.Domain
{
    public class Book
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public string Author { get; set; }
        public int Score {get; set;}
        public ICollection<Review> Reviews { get; set; }
    }
}