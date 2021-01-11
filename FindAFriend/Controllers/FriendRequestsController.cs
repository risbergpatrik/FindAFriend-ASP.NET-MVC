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
    public class FriendRequestsController : AuthenticationController
    {
        private readonly ApplicationDbContext _context;

        //TargetProfile är en global variabel som lagrar det profilobjekt som applikationen ibland behöver
        public static Profile TargetProfile { get; set; }
        public static int RequestCount { get; set; }

    public FriendRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Hämtar alla vänförfrågningar där den inloggade användaren står som Recipient
        public async Task<IActionResult> Index()
        {
            return View(await _context.FriendRequests.Where(fr => fr.Recipient.Equals(User.Identity.Name)).ToListAsync());  
        }

        public IActionResult Create(string id)
        {
            Profile Recipient = _context.Profile.Single(e => e.UserID == id);
            TargetProfile = Recipient;
            return View();
        }

        //Innan ett FriendRequest-objekt lagras i databasen kontrolleras det att en likadan request finns och att de två användarna inte redan är vänner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestID,Sender,Recipient")] FriendRequests friendRequests, string id)
        {
            Profile Recipient = _context.Profile.Single(e => e.UserID == id);
            if (ModelState.IsValid)
            {
                
                friendRequests.Sender = User.Identity.Name;
                friendRequests.Recipient = id;

                bool notEligibleFriends = false;

                FriendRequests friendRequest = _context.FriendRequests.FirstOrDefault(fr => fr.Recipient == id && fr.Sender == User.Identity.Name);

                Friends friend = _context.Friends.FirstOrDefault(f => f.User == User.Identity.Name && f.FriendWith == id);
                Friends friends = _context.Friends.FirstOrDefault(f => f.User == id && f.FriendWith == User.Identity.Name);

                if (friendRequest != null || friend != null || friends != null)
                {
                    notEligibleFriends = true;
                }
                if(notEligibleFriends == false)
                {
                    _context.Add(friendRequests);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(friendRequests);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendRequests = await _context.FriendRequests
                .FirstOrDefaultAsync(m => m.RequestID == id);
            if (friendRequests == null)
            {
                return NotFound();
            }
            TargetProfile = _context.Profile.FirstOrDefault(p => p.UserID == friendRequests.Sender);
      
            return View(friendRequests);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friendRequests = await _context.FriendRequests.FindAsync(id);
            _context.FriendRequests.Remove(friendRequests);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
