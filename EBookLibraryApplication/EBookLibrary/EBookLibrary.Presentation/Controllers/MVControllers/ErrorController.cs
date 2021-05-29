using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<Error> _logger;
        private static string errPath, errString = "";

        public ErrorController(ILogger<Error> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [Route("/Error/{statusCode}")]
        public IActionResult ErrorHandler(int statusCode)
        {

            switch (statusCode)
            {
                case 404:
                    var statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    errPath = statusDetails.OriginalPath;
                    errString = statusDetails.OriginalQueryString;
                    _logger.LogError($"{errPath}, {errString}");
                    break;

            }

            return RedirectToAction("");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotFoundPage()
        {
            ViewBag.ErrorPath = errPath;
            ViewBag.ErrorString = errString;
            return View();
        }
    }
}

