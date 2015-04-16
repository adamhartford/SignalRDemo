﻿using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin;
using System.Collections.Generic;

namespace SignalRDemo
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string url = "http://localhost:8080";
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
			app.UseCors (CorsOptions.AllowAll);
			app.MapSignalR ();
		}
	}

	// Simple example...

	public class SimpleHub : Hub
	{
		public void SendSimple(string message, string detail)
		{
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
		public void SendComplex(ComplexMessage message) 
		{
			Clients.All.notifyComplex (message);
		}
	}
}   