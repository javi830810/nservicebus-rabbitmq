using System;
using System.Collections.Generic;
using ScaleBridge.Message.Event;

using NLog;
using NServiceBus;

namespace ScaleBridge.Web
{
    
	public class JotformMessageController : BaseController
    {
		public Logger Logger { get; set; }
		public IBus Bus { get; set; }
		public JotformMessageController()
		{
			Logger = LogManager.GetLogger(GetType().FullName);
		}

		public string Get()
		{
			Logger.Info("Get METHOD");
			return "Hello world";
		}

		public void Post(ScaleBridge.Message.Event.JotformBaseMessage formData)
		{

			try{
				Logger.Info("Message received");

				Logger.Info(string.Format("formID: {0}", formData.FormID));
				Logger.Info(string.Format("submissionID: {0}", formData.SubmissionID));
				Bus.Send("ScaleBridge.Transform", formData);
			}
			catch(Exception ex){
				Logger.Error(ex.Message);
				Logger.Error(ex.StackTrace);
				throw;
			}
		}

    }
}
