using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Nancy;

namespace SignalRDemo
{
	class MainClass
	{
		public static void Main(string[] args)
		{
            // 127.0.0.1 myserver.com in /etc/hosts
            // Or change myserver.com to localhost
            // Using myserver.com instead of localhost to capture requests in Charles Proxy
			string url = "http://myserver.com:8080";
			using (WebApp.Start (url)) {
				Console.WriteLine ("Server running on {0}", url);
				Console.ReadLine ();
			}
		}
	}

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Change to false to use a persistent connection.
			bool useHubs = true;
			
			app.UseCors (CorsOptions.AllowAll);

			if (useHubs) {
				app.MapSignalR ();
			} else {
				app.MapSignalR<MyConnection> ("/echo");
			}

            app.UseNancy ();
		}
	}

    public class TestModule: NancyModule
    {
        public TestModule() 
        {
            Get ["/Test"] = _ => {
                return View["Demo"];
            };
        }
    }

	// Simple example...
    [CustomAuthorize]
	public class SimpleHub : Hub
	{
		public void SendSimple(string message, string detail)
		{
            var clients = Clients.All;
			Clients.All.notifySimple (message, detail);
		}
	}

	// "Complex" example...

	public class ComplexMessage
	{
		public int MessageId { get ;set ;}
		public string Message { get; set; }
		public string Detail { get; set; }
		public IEnumerable<String> Items { get; set; }
	}

	public class ComplexHub : Hub
	{
        public override Task OnConnected ()
        {
            // foo = bar
            string foo = Context.QueryString ["foo"];
            return base.OnConnected ();
        }
        
		public void SendComplex(ComplexMessage message) 
		{
			Clients.All.notifyComplex (message);
		}
	}

	// Persistent Connection...

	public class MyConnection : PersistentConnection 
	{
		protected override Task OnReceived(IRequest request, string connectionId, string data) 
		{
			return Connection.Broadcast(data);
		}
	}

    // Authorization... Just to demonstrate API and debug context/request objects.

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
        {
            return true;
        }

        public override bool AuthorizeHubConnection (Microsoft.AspNet.SignalR.Hubs.HubDescriptor hubDescriptor, IRequest request)
        {
             return true;
        }

        public override bool AuthorizeHubMethodInvocation (Microsoft.AspNet.SignalR.Hubs.IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            return true;
        }
    }
}   