using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.BookVMs
{
    public class UploadPhotoVM
    {
        public IFormFile BookPhoto { get; set; }
        public string BookId { get; set; }
    }
}
