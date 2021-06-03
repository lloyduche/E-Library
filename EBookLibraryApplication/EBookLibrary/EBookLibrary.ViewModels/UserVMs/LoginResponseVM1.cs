using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.UserVMs
{
    public class LoginResponseVM
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
