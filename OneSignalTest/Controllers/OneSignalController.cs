using Microsoft.AspNetCore.Mvc;
using OneSignalTest.Data;
using OneSignalTest.Models;
using OneSignalTest.Services;

namespace OneSignalTest.Controllers
{
    
    [ApiController]
    [Route("api/onesignal")]
    public class OneSignalController : ControllerBase
    {

        private ApplicationDbContext _context;

        public OneSignalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterPlayerId([FromBody] OneSignalUser user)
        {
            Console.WriteLine("RegisterPlayerId");
            var existingUser = _context.OneSignalUsers.FirstOrDefault(u => u.PlayerId == user.PlayerId);

            // Console.WriteLine(existingUser);
            
            if (existingUser == null)
            {
                _context.OneSignalUsers.Add(user);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Player ID registered"});
            }
            return Ok(new { message = "User already exists" });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateExternalUserId([FromBody] OneSignalUser user)
        {
            Console.WriteLine("UpdateExternalUserId");
            var existingUser = _context.OneSignalUsers.FirstOrDefault(u => u.PlayerId == user.PlayerId);

            if (existingUser == null)
            {
                return Ok(new { message = "User does not exist" });
            }
            existingUser.ExternalUserId = user.ExternalUserId;
            await _context.SaveChangesAsync();
            return Ok(new { message = "User updated" });

        }
        

        [HttpPost("ping")]
        public async Task<IActionResult> Ping([FromQuery] string message)
        {
            return Ok(new { message = "Pong! " + message });
        }
        
        
        
    }
}