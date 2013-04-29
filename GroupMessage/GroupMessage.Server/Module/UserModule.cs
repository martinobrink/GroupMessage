using System;
using System.Text;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using MongoDB.Driver;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;

namespace GroupMessage.Server.Module
{
    public class UserModule : ModuleBase
    {
        private readonly UserRepository _userRepository;

        public UserModule(UserRepository userRepository) : base("groupmessage")
        {
            _userRepository = userRepository;

            Get["/user"] = _ =>
                {
                    var stringBuilder = new StringBuilder();
                    foreach (var user in _userRepository.Users.AsQueryable())
                    {
                        stringBuilder.AppendLine(string.Format("<li>Name: {0} {1}, Email: {2} </li>", user.Name, user.SurName, user.Email));
                    }
                    return "<html>Nancy says that all users are: <br><ul>" + stringBuilder.ToString() + "</ul></html>";
                };

            Post["/user"] = parameters =>
                {
                    var user = this.Bind<User>(); //deserialize request data into User class
                    _userRepository.Create(user);
                    var userString = String.Format("Name: {0} {1}, Email: {2}", user.Name, user.SurName, user.Email);
                    return string.Format("<html>Nancy says that user {0} was saved.</html>", userString);
                };
        }
    }
}
