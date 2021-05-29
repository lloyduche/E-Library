using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EBookLibrary.Commons.ExceptionHandler
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string msg): base(msg)
        {

        }
    }
}
