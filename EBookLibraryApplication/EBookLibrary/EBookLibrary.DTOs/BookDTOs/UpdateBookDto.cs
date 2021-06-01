using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibrary.DTOs.BookDTOs
{
    public class UpdateBookDto
    {
        public string Title { get; set; }

        public string Publisher { get; set; }

        public string Isbn { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }

        public string CopiesAvailable { get; set; }

        public DateTime DatePublished { get; set; }

        public string CategoryId { get; set; }
    }
}
