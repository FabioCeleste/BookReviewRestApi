using AutoMapper;
using BookReview.Application.Dtos;
using BookReview.Domain;

namespace BookReview.Application.Helpers
{
    public class BookReviewProfile : Profile
    {
        public BookReviewProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserNoPasswordDto>().ReverseMap();
        }
    }
}