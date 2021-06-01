using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.BookDTOs
{
    public class FindBookByCategoryDto
    {
        public string Title { get; set; }

        public string AvatarUrl { get; set; }

        public string Author { get; set; }

        public string CopiesAvailable { get; set; }

        public string Ratings { get; set; }

        public string Category { get; set; }
    }
}
