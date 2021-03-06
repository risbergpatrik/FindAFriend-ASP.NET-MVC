﻿using FindAFriend.Models;
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

        //När HomeIndex visas görs en kontroll som kollar om den inloggade användaren har en profil
        //Finns det en profil registrerad sätts HasProfile till true och alla vyer blir tillgängliga.
        public IActionResult Index()
        {
            try
            {
                if (User.Identity.Name != null)
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
                }
            }
            catch (Exception e)
            {
                
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}