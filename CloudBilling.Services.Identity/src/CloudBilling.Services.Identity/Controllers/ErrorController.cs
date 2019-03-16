using CloudBilling.Services.Identity.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CloudBilling.Services.Identity.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IHostingEnvironment _environment;

        public ErrorController(IIdentityServerInteractionService interactionService, IHostingEnvironment environment)
        {
            _interactionService = interactionService;
            _environment = environment;
        }

        public async Task<IActionResult> Index(string errorId)
        {
            var message = await _interactionService.GetErrorContextAsync(errorId);

            var viewModel = new ErrorViewModel
            {
                Error = message?.Error,
                ErrorDescription = _environment.IsDevelopment() ? message?.ErrorDescription : null
            };

            return View("Error", viewModel);
        }
    }
}
