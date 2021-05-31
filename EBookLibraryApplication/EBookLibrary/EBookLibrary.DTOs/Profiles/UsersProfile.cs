using AutoMapper;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Models;

namespace EBookLibrary.Commons.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<Rating, RatingsDto>().ReverseMap();
            CreateMap<Review, ReviewsDto>().ReverseMap();
            CreateMap<Book, FindBookDto>()
                .ForMember(book => book.Reviews, findBook => findBook.MapFrom(book => book.Reviews))
                .ForMember(book => book.Ratings, findBook => findBook.MapFrom(book => book.Ratings))
                .ForMember(book=> book.Category, findBook => findBook.MapFrom(book=> book.Category.Name));
        }
    }
}
