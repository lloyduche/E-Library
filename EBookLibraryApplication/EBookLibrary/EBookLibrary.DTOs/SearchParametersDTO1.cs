using System;

namespace EBookLibrary.DTOs
{
    public class SearchParametersDTO1
    {
        public string Query { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
