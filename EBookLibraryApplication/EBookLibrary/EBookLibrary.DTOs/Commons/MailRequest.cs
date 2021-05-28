using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.Commons
{
    public class MailRequest
    {
        public string Name { get; set; }
        public string  RecipientMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }
    }
}
