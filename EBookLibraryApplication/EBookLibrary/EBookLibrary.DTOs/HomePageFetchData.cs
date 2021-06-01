using System;

namespace EBookLibrary.DTOs
{
    public class HomePageFetchData
    {
        public int PageNumberForMostRecent { get; set; }

        public int PageNumberForMostPopular { get; set; }

        public int PageSize { get; set; }
    }
}
