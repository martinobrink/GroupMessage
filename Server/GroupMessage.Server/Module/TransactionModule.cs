using Nancy;
using GroupMessage.Server.Repository;
using GroupMessage.Server.Module;
using MongoDB.Driver.Linq;
using System.Linq;

namespace GroupMessage.Server
{
    public class TransactionModule: ModuleBase
    {
        private readonly MessageStatusRepository _messageStatusRepository;
        
        public TransactionModule(MessageStatusRepository messageStatusRepository) : base("groupmessage")
        {
            _messageStatusRepository = messageStatusRepository;
            
            Get["/transaction"] = _ => Response.AsJson(_messageStatusRepository.Statuses.AsQueryable().ToList());
        }
    }
}

