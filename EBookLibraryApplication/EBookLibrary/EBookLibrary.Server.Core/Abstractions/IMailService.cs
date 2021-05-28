using EBookLibrary.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
