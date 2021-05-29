using EBookLibrary.DTOs.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IFileUpload
    {
        UploadAvatarResponse UploadAvatar(IFormFile file);
    }
}
