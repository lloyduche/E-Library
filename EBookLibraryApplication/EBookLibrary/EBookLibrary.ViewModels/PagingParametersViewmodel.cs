using System;

namespace EBookLibrary.ViewModels
{
    public class PagingParametersViewModel
    {
        public int PageSize { get; set; } = 5;

        public int PageNumberForMostPopular { get; set; } = 1;

        public int PageNumberForMostRecent { get; set; } = 1;
    }
}
