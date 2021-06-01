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

        public ICollection<T> Result { get; set; }

        public int PreviousPage
        {
            get
            {
                return CurrentPage == 1 ? 1 : CurrentPage - 1;
            }
        }

        public int NextPage
        {
            get
            {
                return CurrentPage == TotalPages ? TotalPages : CurrentPage + 1;
            }
        }

        public PagedResult()
        {
            Result = new List<T>();
        }
    }
}
