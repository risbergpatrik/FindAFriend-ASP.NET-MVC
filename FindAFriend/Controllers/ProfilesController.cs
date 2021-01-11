using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FindAFriend.Data;
using FindAFriend.Models;


namespace FindAFriend.Controllers
{
    public class ProfilesController : AuthenticationController
    {
        private readonly ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Returnerar en detaljerad vy för den angedda profilen

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .FirstOrDefaultAsync(m => m.ProfileID == id);
            if (profile == null)
            {
                return NotFound();
            }

            FriendRequestsController.TargetProfile = profile;

            //Hämtar ut profilbilden som hör till profilen som ska visas
            ImageModel profilePic = _context.ImageModel.FirstOrDefault(i => i.UserEmail == profile.UserID + i.ImageExtension);
            if (profilePic != null)
            {
                string profilePicExtension = profilePic.ImageExtension;
                ViewData["extension"] = profilePicExtension;
            }
            return View(profile);
        }
        //Returnerar en detaljerad vy för den angedda profilen när man går in på den från vänlistan
        public async Task<IActionResult> FriendDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (profile == null)
            {
                return NotFound();
            }
            FriendRequestsController.TargetProfile = profile;
            ImageModel profilePic = _context.ImageModel.FirstOrDefault(i => i.UserEmail == profile.UserID + i.ImageExtension);
            if (profilePic != null)
            {
                string profilePicExtension = profilePic.ImageExtension;
                ViewData["extension"] = profilePicExtension;
            }
            return View(profile);
        }

        //Returnerar vyen för ens egna profil
        public async Task<IActionResult> MyDetails()
        {
            string userID = User.Identity.Name;
            if (userID == null)
            {
                return View("Create");
            }

            var profile = await _context.Profile
                .FirstOrDefaultAsync(m => m.UserID == userID);
            if (profile == null)
            {
                return View("Create");
            }

            ImageModel profilePic = _context.ImageModel.FirstOrDefault(i => i.UserEmail == userID + i.ImageExtension);
            if (profilePic != null)
            {
                string profilePicExtension = profilePic.ImageExtension;
                ViewData["extension"] = profilePicExtension;
            }
            return View(profile);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileID,Name,Birthday,Description,City,UserID")] Profile profile)
        {

            if (ModelState.IsValid)
            {
                profile.UserID = User.Identity.Name;
                _context.Add(profile);
                await _context.SaveChangesAsync();
                AuthenticationController.HasProfile = true;
                return RedirectToAction(nameof(MyDetails));
            }
            return View(profile);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileID,Name,Birthday,Description,City,UserID")] Profile profile)
        {
            if (id != profile.ProfileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.ProfileID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyDetails));
            }
            return View(profile);
        }

        private bool ProfileExists(int id)
        {
            return _context.Profile.Any(e => e.ProfileID == id);
        }
        
        //Returnerar en vy för alla profiler i databasen
        //Kallas också via sökfunktionen, hämtar då alla profiler med ett namn som innehåller söksträngen
        public ActionResult Index(string searchName)
        {
            var profiles = from pr in _context.Profile select pr;

            if (!String.IsNullOrEmpty(searchName))
            {
                profiles = profiles.Where(c => c.Name.Contains(searchName));
            }
            List<Profile> profilesList = profiles.ToList();

            return View(profilesList);
        }
    }
}
