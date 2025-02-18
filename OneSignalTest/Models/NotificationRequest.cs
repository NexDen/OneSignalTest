namespace OneSignalTest.Models
{
    public class NotificationRequest
    {
        public List<string> PlayerIds { get; set; }
        
        public List<string> ExternalUserIds { get; set; }
        public string Message { get; set; }
    }
}