using EBookLibrary.Client.Core.Implementations;
using EBookLibrary.ViewModels;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class HomeController : Controller
    {
        public IClientBookService _bookService;

        public HomeController(IClientBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(int PgNumberForMostRecent = 1, int PgNumberForMostPopular = 1, int pgSize = 5)
        {
            PagingParametersViewModel pagingParams = new PagingParametersViewModel
            {
                PageNumberForMostRecent = PgNumberForMostRecent,
                PageNumberForMostPopular = PgNumberForMostPopular,
                PageSize = pgSize
            };
            var result = await _bookService.GetHomePageData(pagingParams);

            return View(result);
        }
    }
}