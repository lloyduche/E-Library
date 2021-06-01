using AutoMapper;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Models;

namespace EBookLibrary.Commons.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, RegisterDTO>();
            CreateMap<RegisterDTO, User>();
             CreateMap<Book, UpdateBookDto>();
            CreateMap<UpdateBookDto, Book>();

            CreateMap<AddBookDto, Book>();
            CreateMap<Book, AddBookDto>();

            CreateMap<Rating, AddRatingDto>();
            CreateMap<AddRatingDto, Rating>();

            CreateMap<Rating, AddRatingResponseDto>();
            CreateMap<AddRatingResponseDto, Rating>();

            CreateMap<Review, AddReviewDto>();
            CreateMap<AddReviewDto, Review>();

            CreateMap<Review, AddReviewResponseDto>();
            CreateMap<AddReviewResponseDto, Review>();


            CreateMap<Book, AddBookResponseDto>();
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<Rating, RatingsDto>().ReverseMap();
            CreateMap<Review, ReviewsDto>().ReverseMap();
            CreateMap<Book, FindBookDto>()
                .ForMember(book => book.Reviews, findBook => findBook.MapFrom(book => book.Reviews))
                .ForMember(book => book.Ratings, findBook => findBook.MapFrom(book => book.Ratings))
                .ForMember(book => book.Category, findBook => findBook.MapFrom(book => book.Category.Name));

            CreateMap<Book, FindBookBySearchDTO>().ReverseMap();

        }
    }
}
