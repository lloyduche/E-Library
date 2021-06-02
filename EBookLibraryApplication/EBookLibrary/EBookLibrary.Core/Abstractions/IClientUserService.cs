using EBookLibrary.ViewModels;
using EBookLibrary.ViewModels.Common;
using EBookLibrary.ViewModels.UserVMs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Abstractions
{
    public interface IClientUserService
    {
        Task<ExpectedResponse<UserDashboardViewModel>> GetUserById(string Id);

        Task<ExpectedResponse<int>> GetUsersCount();
    }
}
