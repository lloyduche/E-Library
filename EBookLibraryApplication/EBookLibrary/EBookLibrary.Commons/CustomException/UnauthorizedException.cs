using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.Commons.CustomException
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string msg) : base(msg)
        {

        }
    }
}
