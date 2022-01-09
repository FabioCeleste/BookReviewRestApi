using System.Linq;
using System.Threading.Tasks;
using BookReview.Domain;
using BookReview.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Persistence
{
    public class ReviewPersist : IReviewPersist
    {
        public DataContext _context;
        public ReviewPersist(DataContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;        
        }
        public async Task<Review[]> GetAllReviewsAsync()
        {
            IQueryable<Review> query = _context.Reviews;
            query = query.OrderBy(r => r.Id).Include(r => r.Book);
            return await query.ToArrayAsync();
        }

        public async Task<Review[]> GetAllReviewsByBook(Book book)
        {
            IQueryable<Review> query = _context.Reviews;
            query = query.OrderBy(r => r.Id).Where(r => r.Book == book);
            return await query.ToArrayAsync();
        }

        public async Task<Review[]> GetAllReviewsByScoreAsync(int score)
        {
            IQueryable<Review> query = _context.Reviews;
            query = query.OrderBy(r => r.Id).Where(r => r.Score == score);
            return await query.ToArrayAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            IQueryable<Review> query = _context.Reviews;
            query = query.OrderBy(r => r.Id).Where(r => r.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}