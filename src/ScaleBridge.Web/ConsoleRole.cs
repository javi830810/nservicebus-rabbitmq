using System;
using Microsoft.Owin.Hosting;

namespace ScaleBridge.Web
{
    public class ConsoleRole
     {
        public static void Main()
        {
			var url = "http://localhost:8081";
            Console.WriteLine("Initializing ScaleBridge web server on " + url);
            using (WebApp.Start<Startup>(new StartOptions(url)))
            {
                Console.WriteLine("Press q to Quit");
                while (Console.ReadKey().KeyChar != 'q') ;
                Console.WriteLine("ScaleBridge web server terminated.");
            }
        }
    }
}
