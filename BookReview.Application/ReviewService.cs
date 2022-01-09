using System;
using System.Threading.Tasks;
using AutoMapper;
using BookReview.Application.Dtos;
using BookReview.Application.Interfaces;
using BookReview.Domain;
using BookReview.Persistence.Interfaces;

namespace BookReview.Application
{
    public class ReviewService : IReviewService
    {
        public readonly IReviewPersist _reviewPersist;
        public readonly IGeralPersist _geralPersist;
        public readonly IBookPersist _bookPersist;
        public readonly IMapper _mapper;

        public ReviewService(IGeralPersist geralPersist, IReviewPersist reviewPersist, IBookPersist bookPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _reviewPersist = reviewPersist;
            _bookPersist = bookPersist;
            _mapper = mapper;
        }

        //TO-DO
        //better way to add the review and book toghter
        public async Task<ReviewDto> AddReview(ReviewDto model, int bookId)
        {
            try
            {
                var review = _mapper.Map<Review>(model);

                _geralPersist.Add<Review>(review);
                var book = await _bookPersist.GetBookByIdAsync(bookId);
                if (await _geralPersist.SaveChangeAsync()){
                    model.Book = book;
                    var returnReview = await UpdateReview(model, model.Id);

                    return returnReview;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteReview(int reviewId)
        {
            try
            {
                var review = await _reviewPersist.GetReviewByIdAsync(reviewId);
                if (review == null) return false;
                _geralPersist.Delete(review);

                if (await _geralPersist.SaveChangeAsync()){
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReviewDto> UpdateReview(ReviewDto model, int reviewId)
        {
            try
            {
                var findReview = await _reviewPersist.GetReviewByIdAsync(reviewId);
                if (findReview == null) return null;

                model.Id = findReview.Id;

                var modelToUpdate = _mapper.Map<Review>(model);

                _geralPersist.Update<Review>(modelToUpdate);

                if (await _geralPersist.SaveChangeAsync()){
                    var reviewFind = await _reviewPersist.GetReviewByIdAsync(modelToUpdate.Id);
                    var returnReview = _mapper.Map<ReviewDto>(reviewFind);

                    return returnReview;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReviewDto[]> GetAllReviewsAsync()
        {
            try
            {
                var reviews = await _reviewPersist.GetAllReviewsAsync();
                var returnReviews = _mapper.Map<ReviewDto[]>(reviews);
                if (reviews == null) return null;
                return returnReviews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReviewDto[]> GetAllReviewsByBook(string bookTitle)
        {
            try
            {
                var book = await _bookPersist.GetAllBooksByTitleAsync(bookTitle);
                var reviews = await _reviewPersist.GetAllReviewsByBook(book);
                var returnReviews = _mapper.Map<ReviewDto[]>(reviews);
                if (reviews == null) return null;
                return returnReviews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReviewDto[]> GetAllReviewsByScoreAsync(int score)
        {
            try
            {
                var reviews = await _reviewPersist.GetAllReviewsByScoreAsync(score);
                var returnReviews = _mapper.Map<ReviewDto[]>(reviews);
                if (reviews == null) return null;
                return returnReviews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            try
            {
                var review = await _reviewPersist.GetReviewByIdAsync(id);
                if (review == null) return null;

                var reviewReturn = _mapper.Map<ReviewDto>(review);
                return reviewReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}