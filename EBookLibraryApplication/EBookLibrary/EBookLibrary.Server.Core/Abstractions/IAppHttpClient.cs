using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IAppHttpClient<T> where T : class
    {
        Task<IEnumerable<T>> Get(string Uri);
        Task<T> Create(string Uri, T model);
        Task<bool> Update(string Uri, HttpContent model);
        Task<T> UploadPhoto(string Uri, T model);
    }
}
