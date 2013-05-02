using System;
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

            //phone updates: phoneNumber + token + deviceOs
            Put["/user/{phoneNumber}"] = parameters =>
            {
                User user = null;
                try
                {
                    user = this.Bind<User>();
                }
                catch (Exception)
                {
                    return new Response().Create(HttpStatusCode.BadRequest, "Illegal json received: " + Request.Body.GetAsString());
                }

                if (user == null || String.IsNullOrEmpty(user.PhoneNumber))
                {
                    return new Response().Create(HttpStatusCode.BadRequest, "Did you forget to set PhoneNumber? Json received: " + Request.Body.GetAsString());
                }

                var existingUser = _userRepository.GetByPhoneNumber(user.PhoneNumber);
                if (existingUser == null)
                {
                    return new Response().Create(HttpStatusCode.BadRequest, "User with phone number specified was not found");
                }

                existingUser.LastUpdate = DateTime.UtcNow;
                existingUser.DeviceToken = user.DeviceToken;
                existingUser.DeviceOs = user.DeviceOs;
                _userRepository.Users.Save(existingUser);
                return Response.AsJson(existingUser);
            };

            Delete["/user/{phoneNumber}"] = parameters => 
                {
                    var phoneNumber = parameters["phoneNumber"];

                    _userRepository.DeleteByPhoneNumber(phoneNumber);

                    return new Response().Create(HttpStatusCode.OK, "");
                };
        }
    }
}
