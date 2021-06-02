using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.BookDTOs
{
    public class SearchTermDto
    {
        public string Category { get; set; }
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public DateTime YearPublished { get; set; }
    }
}
