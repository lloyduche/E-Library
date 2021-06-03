using EBookLibrary.DTOs;
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
        Task<PagedResult<AdminUserViewModel>> GetAllUser(SearchParametersViewModel model);

        Task<ExpectedResponse<int>> GetUsersCount();

        Task<bool> DeleteUser(string id);
        Task<bool> UploadPhoto(UploadUserAvatarViewModel model);
    }
}
