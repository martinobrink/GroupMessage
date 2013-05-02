using System;
using GroupMessage.Server.Model;

namespace GroupMessage.Server
{
	public interface IMessageSender
	{
		SendStatus Send(User user, String text);
	}
}
