using System.Collections.Generic;
using GroupMessage.Server.Communication;

namespace GroupMessage.Server.Test.Module
{
    public class TestMessageSenderFactory : IMessageSenderFactory
    {
        private readonly IMessageSender _testSpyMessageSender;

        public TestMessageSenderFactory(IMessageSender testSpyMessageSender)
        {
            _testSpyMessageSender = testSpyMessageSender;
        }

        public List<IMessageSender> GetMessageSenders()
        {
            return new List<IMessageSender>(new [] { _testSpyMessageSender });
        }
    }
}