using System;
using System.IO;
using GroupMessage.Server.Model;
using NUnit.Framework;
using Nancy;

namespace GroupMessage.Server.Test.Module
{
    [TestFixture]
    public class VersionModuleTest : NancyIntegrationTestBase<User>
    {
        [Test]
        public void GET_ShouldReturnOkAndVersionNumberSetInFile()
        {
            // ARRANGE
            const string versionNumber = "v0.1.2.xxxxxx";
            File.WriteAllText("version.txt", versionNumber);

            // ACT
            var response = Browser.Get("/groupmessage/version");

            // ASSERT
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var versionNumberReturned = response.Body.AsStringNonNancy();
            Assert.That(versionNumberReturned, Is.EqualTo(versionNumber));
        }
    }
}
