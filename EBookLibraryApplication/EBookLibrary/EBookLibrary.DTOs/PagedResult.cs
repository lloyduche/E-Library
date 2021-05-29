using System;
using System.Collections.Generic;

namespace EBookLibrary.DTOs
{
    public class PagedResult<T> where T : class
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public IList<T> Result;

        public PagedResult()
        {
            Result = new List<T>();
        }
    }
}
