using GroupMessage.Server.Model;
using NUnit.Framework;
using System.Linq;
using Nancy;
using System.Collections.Generic;
using GroupMessage.Server.Repository;
using FakeItEasy;
using GroupMessage.Server.Module;

namespace GroupMessage.Server.Test.Module
{
    [TestFixture]
    public class TwilioModuleTest
    {
//        IUserRepository userRepo;
//        User savedUser;
//
//        [SetUp]
//        public void Setup(Nancy.Testing.ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator configurator) 
//        {
//            userRepo = A.Fake<IUserRepository> ();
//            A.CallTo(() => userRepo.GetByPhoneNumber(A<string>.Ignored)).Returns(null);
//            A.CallTo(() => userRepo.Create(A<User>.Ignored)).Invokes(args => savedUser = args.GetArgument<User>(0));
//            configurator.Dependency<UserRepository> (userRepo);
//            new TwilioModule();
//        }
//
//        [Test]
//        public void POST_TwilioSmsFromUnknownNumber_ShouldAddUserToGroup()
//        {
//            string twilioString="SmsStatus=received&Body=Test&From=%2B4561335440";
//
//            Nancy.Testing.BrowserResponse response = Browser.Post("/twilio/sms", twilioString);
//
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            A.CallTo(() => userRepo.GetByPhoneNumber("61335440")).MustHaveHappened();
//            A.CallTo(() => userRepo.Create(A<User>.Ignored)).MustHaveHappened();
//            Assert.That(savedUser.PhoneNumber, Is.EqualTo("+4561335440"));
//        }
    }
}