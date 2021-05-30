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
            CreateMap<Book, FindBookDto>().ReverseMap()
                .ForMember(book=> book.Ratings, findBook=> findBook.MapFrom(book=> book.Ratings))
                .ForMember(book=> book.Reviews, findBook => findBook.MapFrom(book => book.Reviews));
        }
    }
}
