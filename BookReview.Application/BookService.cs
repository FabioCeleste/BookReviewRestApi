using System;
using System.Threading.Tasks;
using AutoMapper;
using BookReview.Application.Dtos;
using BookReview.Application.Interfaces;
using BookReview.Domain;
using BookReview.Persistence;
using BookReview.Persistence.Interfaces;

namespace BookReview.Application
{
    public class BookService : IBookService
    {
        private readonly IBookPersist _bookPersist;
        private readonly IGeralPersist _geralPersist;

        public readonly IMapper _mapper;

        public BookService(IBookPersist bookPersist,
                        IGeralPersist geralPersist,
                        IMapper mapper)
        {
            _bookPersist = bookPersist;
            _geralPersist = geralPersist;
            _mapper = mapper;
        }

        public async Task<BookDto> AddBook(BookDto model)
        {
            try
            {
                var book = _mapper.Map<Book>(model);

                var bookAlreadyExist = await _bookPersist.GetAllBooksByTitleAsync(book.Title);
                var bookAlreadyExistDto = _mapper.Map<BookDto>(bookAlreadyExist);

                if (bookAlreadyExist == null) {
                    _geralPersist.Add<Book>(book);
                    if (await _geralPersist.SaveChangeAsync())
                    {
                        var bookFind = await _bookPersist.GetBookByIdAsync(book.Id);
                        var returnBook = _mapper.Map<BookDto>(bookFind);

                        return returnBook;
                    } 
                    return null;
                }
                throw new Exception (
                   "{Errors: \"book already exist\"}"
                );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookDto> UpdateBook(BookDto model, int bookId)
        {
            try
            {
                Book findBook = await _bookPersist.GetBookByIdAsync(bookId);
                if (findBook == null) return null;

                model.Id = findBook.Id;

                var modelToUpdate = _mapper.Map<Book>(model);

                _geralPersist.Update<Book>(modelToUpdate);
                if (await _geralPersist.SaveChangeAsync())
                {
                    var bookFind = await _bookPersist.GetBookByIdAsync(modelToUpdate.Id);
                    var returnBook = _mapper.Map<BookDto>(bookFind);

                    return returnBook;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            try
            {
                Book findBook = await _bookPersist.GetBookByIdAsync(bookId);
                if (findBook == null) throw new Exception("Book not found!");

                _geralPersist.Delete<Book>(findBook);
                return await _geralPersist.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookDto[]> GetAllBooksAsync()
        {
            try
            {
                var findBooks = await _bookPersist.GetAllBooksAsync();
                if (findBooks == null) throw new Exception("Books not found!");

                var result = _mapper.Map<BookDto[]>(findBooks);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookDto[]> GetAllBooksByAuthorAsync(string author)
        {
            try
            {
                var findBooks = await _bookPersist.GetAllBooksByAuthorAsync(author);
                if (findBooks == null) throw new Exception("Books not found!");

                var result = _mapper.Map<BookDto[]>(findBooks);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookDto[]> GetAllBooksByScoreAsync(int score)
        {
            try
            {
                var findBooks = await _bookPersist.GetAllBooksByScoreAsync(score);
                if (findBooks == null) throw new Exception("Books not found!");

                var result = _mapper.Map<BookDto[]>(findBooks);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookDto[]> GetAllBooksByTitleAsync(string title)
        {
            try
            {
                var findBooks = await _bookPersist.GetAllBooksByTitleAsync(title);
                if (findBooks == null) throw new Exception("Books not found!");

                var result = _mapper.Map<BookDto[]>(findBooks);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            try
            {
                var findBook = await _bookPersist.GetBookByIdAsync(id);
                if (findBook == null) throw new Exception("Books not found!");

                var result = _mapper.Map<BookDto>(findBook);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}