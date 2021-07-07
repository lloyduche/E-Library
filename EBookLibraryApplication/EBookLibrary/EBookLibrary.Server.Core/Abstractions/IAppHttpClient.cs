using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IAppHttpClient
    {
        Task<TResponse> Get<TResponse>(string Uri);
        Task<TResponse> Create<TResponse, TRequest>(string Uri, TRequest model);
        Task<bool> Update<TRequest>(string Uri, TRequest patchDoc);
        Task<bool> UploadPhoto<TResponse>(IFormFile file, string Uri);
        Task<bool> Delete(string Uri);
    }
}
