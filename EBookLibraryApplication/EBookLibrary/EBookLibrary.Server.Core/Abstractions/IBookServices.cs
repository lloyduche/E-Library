using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IBookServices
    {
        Task<Response<FindBookDto>> FindBook(string Id);
    }
}
