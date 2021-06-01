using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class AppHttpClient : IAppHttpClient
    {
        private readonly IConfiguration _config;
        private readonly ApplicationBaseAddress baseAddress;

        public AppHttpClient(IConfiguration config, IOptions<ApplicationBaseAddress> options)
        {
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
            bool result = false;

            var serializedDoc = JsonConvert.SerializeObject(patchDoc);

            var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");

            using var client = CustomHttpClient();
            {
                var response = await client.PatchAsync(Uri, requestContent);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task<TResponse> UploadPhoto<TResponse, TRequest>(string Uri, IFormFile file)
        {
            TResponse result = default;

            using var client = CustomHttpClient();
            {
                byte[] data;
                using (var br = new BinaryReader(file.OpenReadStream()))
                    data = br.ReadBytes((int)file.OpenReadStream().Length);
                ByteArrayContent bytes = new ByteArrayContent(data);

                MultipartFormDataContent multiContent = new MultipartFormDataContent();

                multiContent.Add(bytes, "file", file.FileName);

                var response = await client.PostAsync(Uri, multiContent);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    result = await response.Content.ReadAsAsync<TResponse>();
                }
            }

            return result;
        }

        public HttpClient CustomHttpClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseAddress.BaseAddress);

            return client;
        }
    }
}
