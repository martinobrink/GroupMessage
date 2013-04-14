using System;
using System.Collections.Generic;
using System.Text;
using GroupMessage.Server.Model;
using MongoDB.Driver;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;

namespace GroupMessage.Server.Modules
{
    public class UserModule : ModuleBase
    {
        public UserModule() : base("groupmessage")
        {
            Get["/users"] = _ =>
            {
                var client = new MongoClient();
                var server = client.GetServer();
                var db = server.GetDatabase("test");
                var users = db.GetCollection<User>("users");
                var stringBuilder = new StringBuilder();
                foreach (var user in users.AsQueryable())
                {
                    stringBuilder.AppendLine(string.Format("<li>Name: {0} {1}, Email: {2} </li>", user.Name, user.SurName, user.Email));
                }
                return "<html>Nancy says that all users are: <br><ul>" + stringBuilder.ToString() + "</ul></html>";
            };

            Post["/users"] = parameters =>
            {
                var user = this.Bind<User>();//deserialize request data into User class
                var client = new MongoClient();
                var server = client.GetServer();
                var db = server.GetDatabase("test");
                var users = db.GetCollection<User>("users");
                users.Save(user);
                var userString = String.Format("Name: {0} {1}, Email: {2}", user.Name, user.SurName, user.Email);
                return string.Format("<html>Nancy says that user {0} was saved.</html>", userString);
            };
        }
    }
}
