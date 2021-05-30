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
            var statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                     statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    errPath = statusDetails.OriginalPath;
                    errString = statusDetails.OriginalQueryString;
                    _logger.LogError($"{errPath}, {errString}");
                   return RedirectToAction("Error", new { message = "You seem lost, are you?", statusCode = 404 });

                case 403:
                     statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    errPath = statusDetails.OriginalPath;
                    errString = statusDetails.OriginalQueryString;
                    _logger.LogError($"{errPath}, {errString}");
                   return RedirectToAction("Error", new { message = "Access Denied", statusCode = 403 });

                case 500:
                    statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    errPath = statusDetails.OriginalPath;
                    errString = statusDetails.OriginalQueryString;
                    _logger.LogError($"{errPath}, {errString}");
                    return RedirectToAction("Error", new { message = "Sorry, something went wrong. We are working on it", statusCode = 500 });

                case 401:
                    statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    errPath = statusDetails.OriginalPath;
                    errString = statusDetails.OriginalQueryString;
                    _logger.LogError($"{errPath}, {errString}");
                    return RedirectToAction("Error", new { message = "Unauthorized", statusCode = 401 });

                default:
                    statusDetails = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    errPath = statusDetails.OriginalPath;
                    errString = statusDetails.OriginalQueryString;
                    _logger.LogError($"{errPath}, {errString}");
                    return RedirectToAction("Error", new { message = "You're probably looking for somewhere else.", statusCode = 302 });

            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Error(string message, int statusCode)
        {
            ViewBag.Message = message;
            ViewBag.StatusCode = statusCode;
            return View();
        }
    }
}

