using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FindAFriend.Data;
using FindAFriend.Models;
using FindAFriend.Controllers;

namespace FindAFriend.Controllers
{
    public class FriendsController : AuthenticationController
    {
        private readonly ApplicationDbContext _context;

        public FriendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Returnerar en sammanlagd lista över alla vänskapsrelationer för användaren
        public IActionResult Index()
        {
            List<Friends> kompisListaA = _context.Friends.Where(f => f.User.Equals(User.Identity.Name)).ToList();
            List<Friends> kompisListaB = _context.Friends.Where(f => f.FriendWith.Equals(User.Identity.Name)).ToList();
            kompisListaA.AddRange(kompisListaB);
            return View(kompisListaA);
        }

        public IActionResult Create(int id)
        {
            FriendRequests friendRequest = _context.FriendRequests.FirstOrDefault(fr => fr.RequestID == id);
            Profile requestSender = _context.Profile.FirstOrDefault(p => p.UserID == friendRequest.Sender);
            FriendRequestsController.TargetProfile = requestSender;
            return View();
        }

        //När ett nytt Friends-samband läggs till i databasen så tas den associerade FriendRequesten bort
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FriendshipID,User,FriendWith")] Friends friends)
        {
            if (ModelState.IsValid)
            {
                friends.User = User.Identity.Name;
                friends.FriendWith = FriendRequestsController.TargetProfile.UserID;
                _context.Add(friends);
                foreach (FriendRequests request in _context.FriendRequests)
                {
                    if (_context.FriendRequests.FirstOrDefault(fr => fr.Sender == friends.User) == request && _context.FriendRequests.FirstOrDefault(fr => fr.Recipient == friends.FriendWith) == request)
                    {
                        _context.FriendRequests.Remove(request);
                    }
                    else if (_context.FriendRequests.FirstOrDefault(fr => fr.Sender == friends.FriendWith) == request && _context.FriendRequests.FirstOrDefault(fr => fr.Recipient == friends.User) == request)
                    {
                        _context.FriendRequests.Remove(request);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(friends);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.Friends
                .FirstOrDefaultAsync(m => m.FriendshipID == id);
            if (friends == null)
            {
                return NotFound();
            }

            return View(friends);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friends = await _context.Friends.FindAsync(id);
            _context.Friends.Remove(friends);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
