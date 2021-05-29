using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.ViewModels.UserVMs
{
    public interface IAuthenticationService
    {
       Task<RegistrationResponse> Register(RegisterationViewModel model);
    }
}
