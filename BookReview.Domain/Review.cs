namespace BookReview.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Book Book { get; set; }
        public int Score { get; set; }
    }
}