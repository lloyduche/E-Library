using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.BookDtos
{
    public class ReviewsDto
    {
        public string BookId { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
