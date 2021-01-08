using FindAFriend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FindAFriend.Data;

namespace FindAFriend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var profile = _context.Profile
                .FirstOrDefault(m => m.UserID == User.Identity.Name);
            if (profile != null)
            {
                AuthenticationController.HasProfile = true;
            }
            else
            {
                AuthenticationController.HasProfile = false;
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Create", "Profiles", new { area = "" });
                }
            }
            return View();
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