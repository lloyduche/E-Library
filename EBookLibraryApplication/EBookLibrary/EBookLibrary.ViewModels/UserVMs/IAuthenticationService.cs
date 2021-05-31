using EBookLibrary.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.ViewModels.UserVMs
{
    public interface IAuthenticationService
    {
        Task<ExpectedResponse<string>> Register(Users model);
        Task<UpdateResponse> Update(UpdateViewModel model);
    }
}
