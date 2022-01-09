using System.Threading.Tasks;
using BookReview.Domain;

namespace BookReview.Persistence.Interfaces
{
    public interface IBookPersist
    {
         Task<Book[]> GetAllBooksAsync();
         Task<Book[]> GetAllBooksByAuthorAsync(string author);
         Task<Book> GetAllBooksByTitleAsync(string title);
         Task<Book[]> GetAllBooksByScoreAsync(int score);
         Task<Book> GetBookByIdAsync(int id); 
    }
}