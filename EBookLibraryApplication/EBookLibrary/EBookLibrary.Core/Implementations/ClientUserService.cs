using EBookLibrary.Client.Core.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.Server.Core.Abstractions;
using EBookLibrary.ViewModels;
using EBookLibrary.ViewModels.Common;
using EBookLibrary.ViewModels.UserVMs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Implementations
{
    public class ClientUserService : IClientUserService
    {
        private readonly IAppHttpClient _httpClient;
        public ClientUserService(IServiceProvider serviceProvider)
        {
            _httpClient = serviceProvider.GetRequiredService<IAppHttpClient>();
        }

        public async Task<PagedResult<AdminUserViewModel>> GetAllUser(SearchParametersViewModel model)
        {
            return await _httpClient.Create<PagedResult<AdminUserViewModel>, SearchParametersViewModel>($"api/v1/user/get-all-user", model);
            
        }

        public async Task<ExpectedResponse<UserDashboardViewModel>> GetUserById(string Id)
        {
           return await _httpClient.Get<ExpectedResponse<UserDashboardViewModel>>($"api/v1/user/get-user/{Id}");

        }

        public async Task<ExpectedResponse<int>> GetUsersCount()
        {
            return await _httpClient.Get<ExpectedResponse<int>>("api/v1/user/get-users-count");
        }


        public async Task<bool> DeleteUser(string id)
        {
            var data = await _httpClient.Delete($"api/v1/User/delete-user/{id}");
            return data;

        }
        public async Task<bool> UploadPhoto(UploadUserAvatarViewModel model)
        {
            var data = await _httpClient.UploadPhoto<ExpectedResponse<string>>(model.Avatar, $"http://localhost:44391/api/v1/user/upload-photo/{model.UserId}");
            if (data)
            {
                return true;
            }

            return false;
        }
       
    }
}
