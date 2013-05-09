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
                    string versionNumberFromFile = null;
                    try
                    {
                        versionNumberFromFile = File.ReadAllText("version.txt");
                    }
                    catch (Exception)
                    {
                        //swallowing exception here but failing later
                    }

                    if (String.IsNullOrEmpty(versionNumberFromFile))
                    {
                        return new Response().Create(HttpStatusCode.InternalServerError, "file version.txt could not be found or file was empty!");
                    }

                    return new Response().Create(HttpStatusCode.OK, versionNumberFromFile.Trim());
                };
        }
    }
}
