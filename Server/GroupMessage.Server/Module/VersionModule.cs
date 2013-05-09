using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nancy;

namespace GroupMessage.Server.Module
{
    public class VersionModule : ModuleBase
    {
        public VersionModule() : base("groupmessage")
        {
            Get["/version"] = parameters =>
                {
                    var versionNumberFromFile = File.ReadAllText("version.txt");
                    
                    return new Response().Create(HttpStatusCode.OK, versionNumberFromFile);
                };
        }
    }
}
