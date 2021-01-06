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
    public class FriendRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public static Profile TargetProfile { get; set; }
        public static int RequestCount { get; set; }

    public FriendRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FriendRequests
        public async Task<IActionResult> Index()
        {
            HowManyRequests();
            return View(await _context.FriendRequests.Where(fr => fr.Recipient.Equals(User.Identity.Name)).ToListAsync());
            
        }

        // GET: FriendRequests/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(friendRequests);
        }

        // GET: FriendRequests/Create
        public IActionResult Create(string id)
        {
            Profile Recipient = _context.Profile.Single(e => e.UserID == id);
            TargetProfile = Recipient;
            return View();
        }

        // POST: FriendRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestID,Sender,Recipient")] FriendRequests friendRequests, string id)
        {
            Profile Recipient = _context.Profile.Single(e => e.UserID == id);
            if (ModelState.IsValid)
            {
                
                friendRequests.Sender = User.Identity.Name;
                friendRequests.Recipient = id;
                _context.Add(friendRequests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(friendRequests);
        }

        // GET: FriendRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendRequests = await _context.FriendRequests.FindAsync(id);
            if (friendRequests == null)
            {
                return NotFound();
            }
            return View(friendRequests);
        }

        // POST: FriendRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestID,Sender,Recipient")] FriendRequests friendRequests)
        {
            if (id != friendRequests.RequestID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friendRequests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendRequestsExists(friendRequests.RequestID))
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
            return View(friendRequests);
        }

        // GET: FriendRequests/Delete/5
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

        // POST: FriendRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friendRequests = await _context.FriendRequests.FindAsync(id);
            _context.FriendRequests.Remove(friendRequests);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendRequestsExists(int id)
        {
            return _context.FriendRequests.Any(e => e.RequestID == id);
        }
        public void HowManyRequests()
        {
            List<FriendRequests> lista = _context.FriendRequests.Where(fr => fr.Recipient.Equals(User.Identity.Name)).ToList();
            int countish = lista.Count;
            RequestCount = countish;
        }
        
    }
}
