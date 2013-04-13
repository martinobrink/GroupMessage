using System;
using System.Collections.Generic;
using System.Text;
using GroupMessage.Server.Model;
using Nancy.ModelBinding;

namespace GroupMessage.Server.Modules
{
    public class UserModule : ModuleBase
    {
        private static readonly List<User> _users = new List<User>();//first-shot in-memory user database :)

        public UserModule() : base("groupmessage")
        {
            Get["/users"] = _ =>
            {
                var stringBuilder = new StringBuilder();
                foreach (var user in _users)
                {
                    stringBuilder.AppendLine(string.Format("<li>Name: {0} {1}, Email: {2} </li>", user.Name, user.SurName, user.Email));
                }
                return "<html>Nancy says that all users are: <br><ul>" + stringBuilder.ToString() + "</ul></html>";
            };

            Post["/users"] = parameters =>
            {
                var user = this.Bind<User>();//deserialize request data into User class
                _users.Add(user);
                var userString = String.Format("Name: {0} {1}, Email: {2}", user.Name, user.SurName, user.Email);
                return string.Format("<html>Nancy says that user {0} was saved.</html>", userString);
            };
        }
    }
}
