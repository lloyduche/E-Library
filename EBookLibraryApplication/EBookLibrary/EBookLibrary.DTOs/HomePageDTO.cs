using EBookLibrary.DTOs.BookDTOs;

using System;

namespace EBookLibrary.DTOs
{
    public class HomePageDTO
    {
        public PagedResult<BookCardDTO> Recent { get; set; }

        public PagedResult<BookCardDTO> MostPopular { get; set; }

        public HomePageFetchData PagingParams { get; set; }
    }
}
