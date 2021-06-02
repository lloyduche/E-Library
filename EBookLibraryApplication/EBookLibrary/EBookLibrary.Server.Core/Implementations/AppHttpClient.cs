using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class AppHttpClient : IAppHttpClient
    {
        private readonly IConfiguration _config;
        private readonly ApplicationBaseAddress baseAddress;
        private readonly IHttpContextAccessor HttpContext;

        public AppHttpClient(IConfiguration config, IOptions<ApplicationBaseAddress> options, IServiceProvider serviceProvider)
        {
            HttpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _config = config;
            baseAddress = options.Value;
        }

        public async Task<TResponse> Create<TResponse, TRequest>(string Uri, TRequest model)
        {
            TResponse result = default;
            using var client = CustomHttpClient();
            {
                var response = await client.PostAsJsonAsync(Uri, model);

                result = await response.Content.ReadAsAsync<TResponse>();
            }
            return result;
        }

        public async Task<TResponse> Get<TResponse>(string Uri)
        {
            TResponse result = default;
            using var client = CustomHttpClient();
            {
                var response = await client.GetAsync(Uri);
                result = await response.Content.ReadAsAsync<TResponse>();
            }

            return result;
        }

        public async Task<bool> Update<TRequest>(string Uri, TRequest patchDoc)
        {
            var serializedDoc = JsonConvert.SerializeObject(patchDoc);

            var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");

            using var client = CustomHttpClient();
            {
                var response = await client.PatchAsync(Uri, requestContent);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<TResponse> UploadPhoto<TResponse>(string Uri, IFormFile file)
        {
            using var client = CustomHttpClient();
            {
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpContent content = new StringContent(file.FileName);
                form.Add(CreateFileContent(file.OpenReadStream(), file.FileName, "multipart/form-data"));
                var stream = file.OpenReadStream();
                var response = await client.PostAsync(Uri, form);
                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        private StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "uploadfile",
                FileName = fileName
            };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }

        public HttpClient CustomHttpClient()
        {
            var client = new HttpClient();
            var token = HttpContext.HttpContext.Session.GetString("access_token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            client.BaseAddress = new Uri(baseAddress.BaseAddress);

            return client;
        }

        public async Task<bool> Delete(string Uri)
        {
            using var client = CustomHttpClient();
            {
                var response = await client.DeleteAsync(Uri);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
