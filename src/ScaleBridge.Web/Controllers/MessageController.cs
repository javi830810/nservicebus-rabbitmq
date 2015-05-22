using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using NLog;
using NServiceBus;

namespace ScaleBridge.Web.Controllers
{
    
	public class MessageController : BaseController
    {
		public Logger Logger { get; set; }
		public IBus Bus { get; set; }
		public MessageController()
		{
			Logger = LogManager.GetLogger(GetType().FullName);
		}

		public string Get()
		{
			Logger.Info ("Get METHOD");

			return "Hello world";
		}

		public void Post(Dictionary<string,string> data)
        {
            
        }

    }
}
