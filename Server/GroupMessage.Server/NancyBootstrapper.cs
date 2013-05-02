using System;
using Nancy.ErrorHandling;
using System.Net;
using Nancy;
using Nancy.Conventions;

namespace GroupMessage.Server
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/", @"app"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}

