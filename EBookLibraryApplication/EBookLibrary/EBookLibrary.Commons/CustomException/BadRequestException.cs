using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.Commons.Exceptions
{
    public class BadRequestException: Exception
    {
        public BadRequestException(string msg): base(msg)
        {

        }
    }
}
