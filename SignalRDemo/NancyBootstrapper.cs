using System;
using System.Linq;
using Nancy;
using Nancy.Session;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;

namespace SignalRDemo
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            CookieBasedSessions.Enable (pipelines);
            StaticConfiguration.DisableErrorTraces = false;
        }

        protected override void ConfigureConventions (NancyConventions nancyConventions)

        {
            nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("css"));
            nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("fonts"));
            nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("img"));
            nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("js"));
            base.ConfigureConventions (nancyConventions);
        }
    }
}

