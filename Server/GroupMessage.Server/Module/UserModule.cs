﻿using System;
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

            Put["/user"] = parameters =>
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
                        _userRepository.Create(user);
                        return new Response().Create(HttpStatusCode.Created, user.AsJson());
                    }
                    
                    user.Id = existingUser.Id;
                    _userRepository.Users.Save(user);
                    return Response.AsJson(user);
                };
        }
    }
}
