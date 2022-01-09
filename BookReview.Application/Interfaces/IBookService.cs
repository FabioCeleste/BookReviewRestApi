using System.Threading.Tasks;
using BookReview.Application.Dtos;

namespace BookReview.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> AddBook(BookDto model);
        Task<BookDto> UpdateBook(BookDto model, int bookId);
        Task<bool> DeleteBook(int bookId);
        Task<BookDto[]> GetAllBooksAsync();
        Task<BookDto[]> GetAllBooksByAuthorAsync(string author);
        Task<BookDto[]> GetAllBooksByTitleAsync(string title);
        Task<BookDto[]> GetAllBooksByScoreAsync(int score);
        Task<BookDto> GetBookByIdAsync(int id);
    }
}