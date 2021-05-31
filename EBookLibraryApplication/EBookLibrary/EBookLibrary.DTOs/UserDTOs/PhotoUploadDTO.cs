using Microsoft.AspNetCore.Http;

using System;
using System.ComponentModel.DataAnnotations;

namespace EBookLibrary.DTOs.UserDTOs
{
    public class PhotoUploadDTO
    {
        [Required]
        public IFormFile Photo { get; set; }

        [Required]
        public string Id { get; set; }
    }
}
