using System;
using System.Collections.Generic;
using ScaleBridge.Message.Event;

using NLog;
using NServiceBus;

namespace ScaleBridge.Web
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
			Logger.Info("Get METHOD");

			return "Hello world";
		}

		public void Post(Dictionary<string,string> message)
        {
			try{
				Logger.Info("Message received");

				Bus.Send("ScaleBridge.Transform", new InputMessage(){
					MessageData = message
				});
			}
			catch(Exception ex){
				Logger.Error(ex.Message);
				Logger.Error(ex.StackTrace);
				throw ex;
			}
        }

    }
}
