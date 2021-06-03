using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.UserVMs
{
    public class UpdateUserAvatarViewModel
    {
        public IFormFile Photo { get; set; }
    }
}
