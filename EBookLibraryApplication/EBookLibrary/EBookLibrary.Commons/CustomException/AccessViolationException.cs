using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.Commons.CustomException
{
    public class AccessViolationException: Exception
    {
        public AccessViolationException(string msg) : base(msg)
        {

        }
    }
}
