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
    public class FriendsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Friends
        public async Task<IActionResult> Index()
        {
            List<Friends> kompisar = _context.Friends.Where(f => f.User.Equals(User.Identity.Name)).ToList();
            List<Friends> merKompisar = _context.Friends.Where(f => f.FriendWith.Equals(User.Identity.Name)).ToList();
            foreach(Friends kompis in merKompisar)
            {
                kompisar.Add(kompis);
            }
            return View(kompisar);
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Friends/Create
        public IActionResult Create(int id)
        {
            FriendRequests friendRequest = _context.FriendRequests.FirstOrDefault(fr => fr.RequestID == id);
            Profile requestSender = _context.Profile.FirstOrDefault(p => p.UserID == friendRequest.Sender);
            FriendRequestsController.TargetProfile = requestSender;
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FriendshipID,User,FriendWith")] Friends friends)
        {
            if (ModelState.IsValid)
            {
                friends.User = User.Identity.Name;
                friends.FriendWith = FriendRequestsController.TargetProfile.UserID;
                _context.Add(friends);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(friends);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.Friends.FindAsync(id);
            if (friends == null)
            {
                return NotFound();
            }
            return View(friends);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FriendshipID,User,FriendWith")] Friends friends)
        {
            if (id != friends.FriendshipID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friends);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendsExists(friends.FriendshipID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(friends);
        }

        // GET: Friends/Delete/5
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

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friends = await _context.Friends.FindAsync(id);
            _context.Friends.Remove(friends);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendsExists(int id)
        {
            return _context.Friends.Any(e => e.FriendshipID == id);
        }
    }
}
