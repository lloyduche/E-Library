using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.BookDTOs
{
    public class UploadPhotoDto
    {
        public IFormFile BookPhoto { get; set; }
        public string BookId { get; set; }
    }
}
