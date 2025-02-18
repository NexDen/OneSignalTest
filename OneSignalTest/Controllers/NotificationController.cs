using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OneSignalApi.Model;
using OneSignalTest.Data;
using OneSignalTest.Models;
using OneSignalTest.Services;


namespace OneSignalTest.Controllers
{
    
    [ApiController]
    [Route("api/onesignal")]
    public class NotificationController : ControllerBase
    {
        private readonly OneSignalService _oneSignalService;
        private readonly ApplicationDbContext _context;


        public NotificationController(OneSignalService oneSignalService, ApplicationDbContext context)
        {
            _oneSignalService = oneSignalService;
            _context = context;
        }

        [HttpPost("sendnotification")]
        public async Task<IActionResult> SendNotification([FromBody] OneSignalUser request)
        {
            Console.WriteLine("SendNotification");
            var user = _context.OneSignalUsers.FirstOrDefault(user => user.PlayerId == request.PlayerId);
            
            if (user == null) return BadRequest("No user found");

            Console.WriteLine($"Sending notification to user: {user.PlayerId}");
            var success = await _oneSignalService.SendNotificationAsync("Test Notification", user);
            
            return success ? Ok("Notification Sent") : BadRequest("Error sending notification");
        }
        
    }
    
}

