using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class AppHttpClient<T> : IAppHttpClient<T> where T : class
    {
        private readonly IConfiguration _config;
        private readonly ApplicationBaseAddress baseAddress;

        public AppHttpClient(IConfiguration config, IOptions<ApplicationBaseAddress> options)
        {
            _config = config;
            baseAddress = options.Value;
            
        }

       
     
        public async Task<T> Create(string Uri, T model)
        {
            T result = default;
            using var client = HttpClient();
            {
                var response =await client.PostAsJsonAsync(Uri, model);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    result= await response.Content.ReadAsAsync<T>();
                }
            }

            return result;
               
        }

        public async Task<IEnumerable<T>> Get(string Uri)
        {
            IEnumerable<T> result = new List<T>();

            using var client = HttpClient();
            {
                var response = await client.GetAsync(Uri);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<IEnumerable<T>>();
                }
            }

            return result;
        }

        public async Task<bool> Update(string Uri, HttpContent content)
        {
            bool result = false;

            using var client = HttpClient();
            {
                var response = await client.PatchAsync(Uri,content);

                if(response.StatusCode == HttpStatusCode.NoContent)
                {
                    result = true;
                }
            }

            return result;

        }

        public async Task<T> UploadPhoto(string Uri, T model)
        {
            T result = default;

            using var client = HttpClient();
            {
                var response = await client.PostAsJsonAsync(Uri, model);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    result = await response.Content.ReadAsAsync<T>();
                }
            }

            return result;

        }

       public HttpClient HttpClient()
        {
            var client = HttpClient();

            client.BaseAddress = new Uri(baseAddress.BaseAddress);

            return client;
        }
    }
}
