# SignalRDemo
A basic self-hosted SignalR example. Created to demo [SwiftR](https://github.com/adamhartford/SwiftR "SwiftR"). It also includes a basic web page example using [Nancy](http://nancyfx.org).

You can run this application, browse to http://localhost:8080/Test, run a SwiftR iOS application, and see how actions invoked in iOS trigger client actions on the web page, and vice versa.

## Note
~~I created this on a Mac using Xamarin Studio, running Mono 4.0.1. I'm not using SignalR Core from NuGet, but am instead referencing a custom built Microsoft.AspNet.SignalR.Core.dll due to a Mono 4.0 bug.~~

~~See: http://stackoverflow.com/questions/28288136/running-signalr-self-host-samples-throws-exception-with-latest-mono-dev-branch~~

A custom Microsoft.AspNet.SignalR.Core.dll is no longer necessary with Mono 4.2.
