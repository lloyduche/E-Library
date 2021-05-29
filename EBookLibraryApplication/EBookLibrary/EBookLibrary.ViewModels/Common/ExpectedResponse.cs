using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.Common
{
    public class ExpectedResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }
}
