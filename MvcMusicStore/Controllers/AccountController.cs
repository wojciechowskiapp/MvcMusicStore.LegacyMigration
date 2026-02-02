using System;
using System.Web.Security;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using MvcMusicStore.Application.Common.Interfaces;

namespace MvcMusicStore.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        private void MigrateShoppingCart(string UserName)
        {
            // Associate shopping cart items with logged-in user
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.MigrateCart(UserName);
            Session[ShoppingCart.CartSessionKey] = UserName;
        }
        [HttpGet]

        //
        // GET: /Account/LogOn
        public IActionResult LogOn()
        {
            return View();
        }
        //
        // POST: /Account/LogOn
        [HttpPost]
        public async Task<IActionResult> LogOn([FromBody] LogOnModel model, string returnUrl)
        {
            var result = await _mediator.Send(new AccountLogOnCommand { Model = model, ReturnUrl = returnUrl });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpGet]
        //
        // GET: /Account/LogOff
        public async Task<IActionResult> LogOff()
        {
            var result = await _mediator.Send(new AccountLogOffQuery());
            return result.IsSuccess ? RedirectToAction("Index") : BadRequest();
        }
        [HttpGet]

        //
        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _mediator.Send(new AccountRegisterCommand { Model = model });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        //
        // GET: /Account/ChangePassword
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        //
        // POST: /Account/ChangePassword
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var result = await _mediator.Send(new AccountChangePasswordCommand { Model = model });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpGet]

        //
        // GET: /Account/ChangePasswordSuccess
        public IActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(/* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}