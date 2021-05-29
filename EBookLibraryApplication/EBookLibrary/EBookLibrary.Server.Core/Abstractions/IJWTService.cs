using EBookLibrary.Models;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IJWTService
    {
        public Task<string> GenerateToken(User user);
    }
}
