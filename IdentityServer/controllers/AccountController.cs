using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using System;
using Microsoft.VisualBasic;
using IdentityServer4.Services;

namespace IdentityServer.controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly TestUserStore _users;

        public AccountController(IIdentityServerInteractionService interaction,TestUserStore users)
        {
            _interaction = interaction;
            _users = users;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {
            if (_users.ValidateCredentials(username, password))
            {
                var user = _users.FindByUsername(username);
                var isUser = new IdentityServerUser(user.SubjectId)
                {
                    DisplayName = user.Username,
                    AdditionalClaims = user.Claims
                };

                var principal = isUser.CreatePrincipal();

                await HttpContext.SignInAsync("idsrv", principal);


                bool isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                Console.WriteLine("Authenticated: " + isAuthenticated);
                if (!string.IsNullOrWhiteSpace(returnUrl) &&
                         (Url.IsLocalUrl(returnUrl) || returnUrl.StartsWith("/connect/authorize/callback")))
                {
                    return Redirect(returnUrl);
                }
                return Redirect("~/");
            }

            ViewData["Error"] = "Invalid credentials";
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // optionally retrieve information about the logout context
            var logoutRequest = await _interaction.GetLogoutContextAsync(logoutId);

            // sign out of IdentityServer
            await HttpContext.SignOutAsync("idsrv");

            // redirect back to the client post logout redirect URI
            if (!string.IsNullOrEmpty(logoutRequest?.PostLogoutRedirectUri))
            {
                return Redirect(logoutRequest.PostLogoutRedirectUri);
            }
            return Redirect("http://localhost:4200");
        }
    }
}
