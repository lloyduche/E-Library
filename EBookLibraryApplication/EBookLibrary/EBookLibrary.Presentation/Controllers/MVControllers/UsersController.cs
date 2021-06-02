using EBookLibrary.Client.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class UsersController : Controller
    {

        private readonly IClientUserService _user;

        public UsersController(IClientUserService user)
        {
            _user = user;
        }



        public IActionResult Index()
        {
            return View();
        }

    

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromForm] string id)
        {
            var data = await _user.DeleteUser(id);
            return Redirect("/Dashboard/ManageAccount");
        }
    }
}
