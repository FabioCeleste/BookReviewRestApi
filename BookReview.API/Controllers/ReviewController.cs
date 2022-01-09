using System;
using System.Threading.Tasks;
using BookReview.Application.Dtos;
using BookReview.Application.Interfaces;
using BookReview.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ReviewController : ControllerBase
    {
        public IReviewService _reviewService;

        public ReviewController(IReviewService reviewService, IBookService bookService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var reviews = await _reviewService.GetAllReviewsAsync();
                if (reviews == null) return NotFound("Reviews not found.");
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{reviewId}/bybookid")]
        public async Task<IActionResult> GetById(int reviewId)
        {
            try
            {
                var review = await _reviewService.GetReviewByIdAsync(reviewId);

                if (review == null) return BadRequest("Error to save review.");

                return Ok(review);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{bookTitle}/bybooktitle")]
        public async Task<IActionResult> GetByBook(string bookTitle)
        {
            try
            {
                var review = await _reviewService.GetAllReviewsByBook(bookTitle);

                if (review == null) return BadRequest("Error to save review.");

                return Ok(review);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{score}/bybookscore")]
        public async Task<IActionResult> GetByScore(int score)
        {
            try
            {
                var review = await _reviewService.GetAllReviewsByScoreAsync(score);

                if (review == null) return BadRequest("Error to save review.");

                return Ok(review);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{bookId}/add")]
        public async Task<IActionResult> Post(ReviewDto model, int bookId)
        {
            try
            {
                var review = await _reviewService.AddReview(model, bookId);

                if (review == null) return BadRequest("Error to save review.");

                return Ok(review);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{reviewId}/update")]
        public async Task<IActionResult> Put(ReviewDto model, int reviewId)
        {
            try
            {
                var review = await _reviewService.UpdateReview(model, reviewId);

                if (review == null) return BadRequest("Error to update review.");

                return Ok(review);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{reviewId}/delete")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            try
            {
                var review = await _reviewService.DeleteReview(reviewId);

                if (!review) return BadRequest("Error to delete review.");

                return Ok(review);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}