using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.BookDTOs
{
    public class AddBookResponseDto
    {
       
        public string Id { get; set; }

       
        public string PublicId { get; set; }

       
        public string CategoryId { get; set; }

       
        public string Title { get; set; }

       
        public string Publisher { get; set; }

       
        public string Isbn { get; set; }

      
        public string AvatarUrl { get; set; }

       
        public string Description { get; set; }

       
        public int Pages { get; set; }

       
        public string Author { get; set; }

       
        public string CopiesAvailable { get; set; }

        public DateTime DatePublished { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;  
        
    }
}
