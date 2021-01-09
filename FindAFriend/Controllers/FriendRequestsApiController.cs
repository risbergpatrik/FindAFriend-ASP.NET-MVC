using FindAFriend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindAFriend.Models;

namespace FindAFriend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FriendRequestsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FriendRequestsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getrequestcount")]
        public async Task<ActionResult<IEnumerable<FriendRequests>>> GetFriendRequests()
        {
            List<FriendRequests> friendRequests = _context.FriendRequests.Where(fr => fr.Recipient == User.Identity.Name).ToList();
            return friendRequests;
        }

    }
}
