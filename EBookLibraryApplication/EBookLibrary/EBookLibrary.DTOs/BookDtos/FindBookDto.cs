using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibrary.DTOs.BookDtos
{
    public class FindBookDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

       
        public string Publisher { get; set; }

       
        public string Isbn { get; set; }

       
        public string AvatarUrl { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public string Author { get; set; }

        public string CopiesAvailable { get; set; }

        public DateTime DatePublished { get; set; }

        public ICollection<ReviewsDto> Reviews { get; set; }

        public ICollection<RatingsDto> Ratings { get; set; }

        public string Category { get; set; }
    }
}
