using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindAFriend.Data;
using FindAFriend.Models;
using Microsoft.AspNetCore.Authorization;

namespace FindAFriend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MessagesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MessagesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Hämtar meddelanden där mottagaren är den aktuella profilen som visas
        [HttpGet]
        [Route("loadmessages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessage(string profileUser)
        {
            List<Message> meddelanden = _context.Message.Where(m => m.Recipient == profileUser).ToList();
            List<Message> meddelandenAttDisplaya = new List<Message>();
            foreach(Message m in meddelanden)
            {
                Profile profile = _context.Profile.FirstOrDefault(p => p.UserID == m.Sender);
                m.Sender = profile.Name;
                meddelandenAttDisplaya.Add(m);
            }
            return meddelandenAttDisplaya;
        }

        //Postar ett nytt meddelande i databasen
        //TimeSent sätts till DateTime.Now när meddelandet skickas
        //Recipient och Text hämtas ifrån vyn
        [HttpPost]
        [Route("postmessage")]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {

            Message newMessage = new Message
            {
                Sender = User.Identity.Name,
                Recipient = message.Recipient,
                TimeSent = DateTime.Now,
                Text = message.Text
            };

            _context.Message.Add(newMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = newMessage.ID }, newMessage); 
        }
    }
}
