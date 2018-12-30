using DataProcessing.Core.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataProcessing.Core.Web.Controllers
{
    [AllowAnonymous]
    public class DataProcessAuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                bool LoginStatus = true; // objUser.ValidateLogin(user);

                if (LoginStatus)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login.UserName)
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                    return View();
                }
            }
            else
                return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "DataProcessAuthentication");
        }
    }
}