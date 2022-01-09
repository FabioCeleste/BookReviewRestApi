using System.Threading.Tasks;
using BookReview.Domain;

namespace BookReview.Persistence.Interfaces
{
    public interface IReviewPersist
    {
         Task<Review[]> GetAllReviewsAsync();
         Task<Review[]> GetAllReviewsByScoreAsync(int score);
         Task<Review[]> GetAllReviewsByBook(Book book);
         Task<Review> GetReviewByIdAsync(int id);
    }
}