using System.Linq;
using System.Threading.Tasks;
using BookReview.Domain;
using BookReview.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Persistence
{
    public class BookPersist : IBookPersist
    {
        private readonly DataContext _context;
        public BookPersist(DataContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Book[]> GetAllBooksAsync()
        {
            IQueryable<Book> query = _context.Books;
            query = query.OrderBy(book => book.Id).Include(book => book.Reviews);
            return await query.ToArrayAsync();
        }

        public async Task<Book[]> GetAllBooksByAuthorAsync(string author)
        {
            IQueryable<Book> query = _context.Books;

            query = query.OrderBy(book => book.Id).Where(book => book.Author.ToLower() == author.ToLower());

            return await query.ToArrayAsync();
        }

        public async Task<Book[]> GetAllBooksByScoreAsync(int score)
        {
            IQueryable<Book> query = _context.Books;
            query = query.OrderBy(book => book.Id).Where(book => book.Score == score);
            return await query.ToArrayAsync();
        }

        public async Task<Book> GetAllBooksByTitleAsync(string title)
        {
            IQueryable<Book> query = _context.Books;
            query = query.OrderBy(book => book.Id).Where(book => book.Title.ToLower().Contains(title.ToLower()));
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            IQueryable<Book> query = _context.Books;
            query = query.OrderBy(book => book.Id).Where(book => book.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}