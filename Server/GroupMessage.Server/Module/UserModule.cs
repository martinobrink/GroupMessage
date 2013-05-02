using System;
using System.Text;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;
using Nancy;
using System.Linq;

namespace GroupMessage.Server.Module
{
    public class UserModule : ModuleBase
    {
        private readonly UserRepository _userRepository;

        public UserModule(UserRepository userRepository) : base("groupmessage")
        {
            _userRepository = userRepository;

            Get["/user"] = _ => Response.AsJson(_userRepository.Users.AsQueryable().ToList());

            Post["/user"] = parameters =>
                {
                    var user = this.Bind<User>(); //deserialize request data into User class
                    _userRepository.Create(user);
                    return Response.AsJson(user);
                };
        }
    }
}
