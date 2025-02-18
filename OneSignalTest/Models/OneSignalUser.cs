namespace OneSignalTest.Models
{
    public class OneSignalUser
    {
        public int Id { get; set; }
        public string PlayerId { get; set; } // OneSignal Player ID
        
        public string? ExternalUserId { get; set; }
    }
}