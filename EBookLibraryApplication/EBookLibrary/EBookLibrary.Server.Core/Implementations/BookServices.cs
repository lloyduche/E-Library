﻿using AutoMapper;
using EBookLibrary.Commons.ExceptionHandler;
using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;


        public BookServices(IServiceProvider serviceProvider)
        {
            _bookRepository = serviceProvider.GetRequiredService<IBookRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }


        public async Task<Response<FindBookDto>> FindBook(string Id)
        {
            Response<FindBookDto> response = new Response<FindBookDto>();

            //get reviews and ratings
            var book = await _bookRepository.GetDetailedBook(Id);
            if (book == null)
                throw new NotFoundException("The Book With This Title Cannot Be Found");
            
            var books = _mapper.Map<FindBookDto>(book);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = "Search Successful";
            response.Success = true;
            response.Data = books;

            return response;
        }

       
    }
}