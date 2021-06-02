using System;

namespace EBookLibrary.DTOs
{
    public class SearchParametersDTO1
    {
        public string Query { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 15;
    }
}
