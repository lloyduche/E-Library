using EBookLibrary.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibrary.ViewModels.BookVMs
{
    public class UpdateBookViewModel
    {
        
        public string Id { get; set; }

      
        public string CategoryId { get; set; }

       
        public string Title { get; set; }

       
        public string Publisher { get; set; }

       
        public string Isbn { get; set; }

       
        public string AvatarUrl { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public string Author { get; set; }

        public string CopiesAvailable { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime DatePublished { get; set; }

        public UploadPhotoVM UploadPhotoVM { get; set; }

    }
}
