using System;
using System.Collections.Generic;
using ScaleBridge.Message;

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

				Bus.Send("ScaleBridge.Transform", new EventMessage(){
					MessageType = "jotform_start", //We will hardcode the type for now
					Data = message
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
