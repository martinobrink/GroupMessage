using System;

namespace GroupMessage.Server
{
    public class TwilioMessageSender
    {
        string AccountSID;
        string SenderNumber;
        string AuthToken;

        public TwilioMessageSender ()
        {
            AccountSID = "AC0c600b58e62248e7f62b052a475609d7";
            SenderNumber = "+1 469-275-4652";
            AuthToken = "0507d05d7a50317000d51ca2f1e8699e";

            var client = new TwilioRestClient(accountSid, authToken);
        }


    }
}

