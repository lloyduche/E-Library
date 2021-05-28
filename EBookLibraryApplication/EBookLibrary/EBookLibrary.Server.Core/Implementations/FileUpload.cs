using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EBookLibrary.DTOs.Commons;
using EBookLibrary.Models.Settings;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace EBookLibrary.Server.Core.Implementations
{
    public class FileUpload : IFileUpload
    {
        private readonly CloudinaryConfig _config;
        private readonly Cloudinary _cloudinary;

        public FileUpload(IOptions<CloudinaryConfig> config)
        {
            _config = config.Value;
            Account account = new Account(
                _config.CloudName,
                _config.ApiKey,
                _config.ApiSecret   
             );

            _cloudinary = new Cloudinary(account);
        }
        public UploadAvatarResponse UploadAvatar(IFormFile file)
        {

            var imageUploadResult = new ImageUploadResult();
            using (var fs = file.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, fs),
                    Transformation = new Transformation().Width(300).Height(300)
                .Crop("fill").Gravity("face")
                };
                imageUploadResult = _cloudinary.Upload(imageUploadParams);
            }
            var publicId = imageUploadResult.PublicId;
            var avatarUrl = imageUploadResult.Url.ToString();
            var result = new UploadAvatarResponse
            {
                PublicId = publicId,
                AvatarUrl = avatarUrl
            };
            return result;
        }
    }
}
