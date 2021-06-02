﻿using EBookLibrary.Client.Core.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.Server.Core.Abstractions;
using EBookLibrary.ViewModels;
using EBookLibrary.ViewModels.Common;
using EBookLibrary.ViewModels.UserVMs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
    }
}
