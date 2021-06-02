using AutoMapper;

using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
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

            CreateMap<User, UserDTO>().ReverseMap();

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

            CreateMap<Book, BookCardDTO>().ReverseMap();

            CreateMap<PagedResult<Book>, PagedResult<BookCardDTO>>()
                .ForMember(book => book.Result, dto => dto.MapFrom(book => book.Result));

            CreateMap<User, AdminUserDTO>().ReverseMap();
            CreateMap<PagedResult<User>, PagedResult<AdminUserDTO>>()
                .ForMember(user => user.Result, dto => dto.MapFrom(user => user.Result));

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
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
