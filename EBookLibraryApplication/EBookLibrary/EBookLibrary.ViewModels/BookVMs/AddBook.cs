using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibrary.ViewModels.BookVMs
{
    public class AddBook
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public string AvatarUrl { get; set; }

        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "The property {0} should have not have more than {1} characters")]
        public string Description { get; set; }

        [Required]
        public int Pages { get; set; }

        [Required]
        public string Author { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public string CopiesAvailable { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
