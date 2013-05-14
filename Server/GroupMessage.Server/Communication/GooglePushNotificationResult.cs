namespace GroupMessage.Server.Communication
{
    public class GooglePushNotificationResult
    {
        public long multicast_id { get; set; }
        public bool success { get; set; }
        public bool failure { get; set; }
        public int canonical_ids { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string message_id { get; set; }
        public string error { get; set; }
    }
}