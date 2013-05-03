using System;
using GroupMessage.Server.Model;
using NUnit.Framework;

namespace GroupMessage.Server.Test.Model
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void ShouldRemoveWhiteSpaceFromPhoneNumberWhenNormalizing()
        {
            var user = new User{PhoneNumber="12 13 14 15"};
            Assert.That(user.Normalized().PhoneNumber, Is.EqualTo("12131415"));
        }

        [Test]
        public void ShouldRemovePlus45FromPhoneNumberWhenNormalizing()
        {
            var user = new User{PhoneNumber="+4512131415"};
            Assert.That(user.Normalized().PhoneNumber, Is.EqualTo("12131415"));
        }

        [Test]
        public void ShouldRemoveZeroZero45FromPhoneNumberWhenNormalizing()
        {
            var user = new User{PhoneNumber="004512131415"};
            Assert.That(user.Normalized().PhoneNumber, Is.EqualTo("12131415"));
        }

        [Test]
        public void ShouldNotChangeAnAlreadyNormalizedPhoneNumber()
        {
            var user = new User{PhoneNumber="12131415"};
            Assert.That(user.Normalized().PhoneNumber, Is.EqualTo("12131415"));
        }
    }
}

