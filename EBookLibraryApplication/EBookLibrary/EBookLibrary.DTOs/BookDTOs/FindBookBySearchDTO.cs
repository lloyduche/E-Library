using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.BookDTOs
{
    public class FindBookBySearchDTO
    {

        public string Title { get; set; }

        public string AvatarUrl { get; set; }

        public string Author { get; set; }

        public string CopiesAvailable { get; set; }
    }
}

