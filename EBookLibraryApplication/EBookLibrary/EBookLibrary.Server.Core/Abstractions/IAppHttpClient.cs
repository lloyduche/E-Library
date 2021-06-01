using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IAppHttpClient
    {
        Task<TResponse> Get<TResponse>(string Uri);
        Task<TResponse> Create<TResponse, TRequest>(string Uri, TRequest model);
        Task<bool> Update<TRequest>(string Uri, TRequest patchDoc);
        Task<TResponse> UploadPhoto<TResponse, TRequest>(string Uri, IFormFile file);
        Task<bool> Delete(string Uri);
    }
}
