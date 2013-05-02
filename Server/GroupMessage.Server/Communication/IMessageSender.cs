using System;
using GroupMessage.Server.Model;

namespace GroupMessage.Server
{
	public interface IMessageSender
	{
		void Send(User user, String message);
	}
}
