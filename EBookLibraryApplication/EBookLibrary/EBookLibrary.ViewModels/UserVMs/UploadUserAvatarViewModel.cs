using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.UserVMs
{
    public class UploadUserAvatarViewModel
    {
        public IFormFile Avatar { get; set; }
        public string UserId { get; set; }
    }
}
