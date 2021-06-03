using EBookLibrary.DTOs;
using EBookLibrary.ViewModels.BookVMs;

using System;

namespace EBookLibrary.ViewModels
{
    public class HomePageViewModel
    {
        public PagedResult<BookCardViewModel> Recent { get; set; }

        public PagedResult<BookCardViewModel> MostPopular { get; set; }

        public PagingParametersViewModel PagingParams { get; set; }

        public SearchParametersViewModel1 Search { get; set; }
    }
}
