using System;
using System.ComponentModel.DataAnnotations;

namespace EBookLibrary.DTOs.BookDTOs
{
    public class BookCardDTO
    {
        public string Id { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Isbn { get; set; }

        //[Required
        public string? AvatarUrl { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string CopiesAvailable { get; set; }

        public int Rating { get; set; }
    }
}
