using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IBookServices
    {
        Task<TResponse<FindBookDto>> FindBook(string Id);

        Task<TResponse<IReadOnlyList<FindBookBySearchDTO>>> GetAllBooksWhere(SearchTermDto term);

    }
}
