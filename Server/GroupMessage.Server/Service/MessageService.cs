using System;
using GroupMessage.Server.Repository;
using GroupMessage.Server.Model;
using MongoDB.Driver.Linq;
using System.Linq;


namespace GroupMessage.Server
{
    public class MessageService
    {
        private readonly UserRepository _userRepository;
        private readonly IMessageSender _sender;
        private readonly MessageStatusRepository _messageStatusRepository;

        public MessageService (IMessageSender messageSender, UserRepository userRepository, MessageStatusRepository messageStatusRepository)
        {
            _userRepository = userRepository;
            _sender = messageSender;
            _messageStatusRepository = messageStatusRepository;
        }

        public void initialSend(Message message) {
            var users = _userRepository.Users.AsQueryable().ToList();
            foreach (var user in users) 
            {
                _messageStatusRepository.Create(new MessageStatus{Message=message, User=user});
            }
            
            
            var statuses = _messageStatusRepository.Statuses.AsQueryable<MessageStatus>().Where(s => s.Message.Id == message.Id && s.Status.NumberOfTries == 0);
            foreach (var status in statuses) 
            {
                var sendStatus = _sender.Send(status.User, status.Message.Text);
                status.Status.NumberOfTries++;
                status.Status.Success = sendStatus.Success;
                status.Status.ErrorMessage = sendStatus.ErrorMessage;
                _messageStatusRepository.Update(status);
            }
        }
    }
}

