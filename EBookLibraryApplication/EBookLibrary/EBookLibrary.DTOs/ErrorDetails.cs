using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs
{
    public class ErrorDetails
    {
        public ErrorDetails(int statuCode, string message, string details = null)
        {
            StatusCode = statuCode;
            Message = message;
            Details = details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }



        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
