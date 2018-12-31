using DataProcessing.Application.Authendication;
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
        private readonly IValidateUser _validateUser;
        private readonly ICreateUser _createUser;

        public DataProcessAuthenticationController(IValidateUser validateUser, ICreateUser createUser)
        {
            _validateUser = validateUser;
            _createUser = createUser;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var loginStatus = _validateUser.Validate(login.UserName, login.Password); // objUser.ValidateLogin(user);

                if (loginStatus.status)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login.UserName)
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    TempData["User"] = loginStatus.userName;
                    HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["UserLoginFailed"] = loginStatus.error;
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
        [HttpPost]
        public IActionResult Register(Login login)
        {
            var status = _createUser.Create(login.UserName, login.Password, login.Email).Result;
            if (status)
                return View("Login");
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