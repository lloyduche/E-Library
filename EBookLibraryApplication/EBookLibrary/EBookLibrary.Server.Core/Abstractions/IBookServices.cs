﻿using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IBookServices
    {
        Task<Response<AddBookResponseDto>> AddBook(AddBookDto addbookdto);

        Task<bool> UpdateBook(UpdateBookDto updatebookdto, string Id);

        Task<bool> DeleteBook(string bookid);

        Task<Response<string>> UploadPhoto(UploadPhotoDto uploadphotodto);

        Task<Response<AddRatingResponseDto>> AddRating(AddRatingDto addratingdto);

        Task<Response<AddReviewResponseDto>> AddReview(AddReviewDto addreviewdto);

        PagedResult<BookCardDTO> Search(SearchParametersDTO1 model);

        Task<Response<FindBookDto>> FindBook(string Id);

        HomePageDTO GetHomePageData(HomePageFetchData paging);
    }
}
