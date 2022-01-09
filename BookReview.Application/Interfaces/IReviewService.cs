using System.Threading.Tasks;
using BookReview.Application.Dtos;
using BookReview.Domain;

namespace BookReview.Application.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDto> AddReview(ReviewDto model, int bookId);
        Task<ReviewDto> UpdateReview(ReviewDto model, int reviewId);
        Task<bool> DeleteReview(int reviewId);
        Task<ReviewDto[]> GetAllReviewsAsync();
        Task<ReviewDto[]> GetAllReviewsByScoreAsync(int score);
        Task<ReviewDto[]> GetAllReviewsByBook(string bookTitle);
        Task<ReviewDto> GetReviewByIdAsync(int id);
    }
}