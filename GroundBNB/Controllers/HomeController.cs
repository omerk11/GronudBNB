using GroundBNB.Data;
using GroundBNB.Models;
using GroundBNB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GroundBNB.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteContext _context;

        private readonly ISiteViewsService _siteviews;

        public HomeController(SiteContext context, ISiteViewsService siteViews)
        {
            _context = context;
            _siteviews = siteViews;
        }


        public IActionResult Index()
        {
            this._siteviews.Increment();
            return View();
        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Secured()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
            
        {
            //ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> ValidateAsync(string username, string password, string returnUrl)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == username);
           
            //ViewData["ReturnUrl"] = returnUrl;
            if(user!=null && password==user.Password)
            {
                //claim = properties for user
                //var identity = new ClaimsIdentity();
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim("ID", user.ID.ToString()));

                if (user.IsAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect("/");
            }
            TempData["Error"] = "Error. User Name or password are incorrect";
            return View("login");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
