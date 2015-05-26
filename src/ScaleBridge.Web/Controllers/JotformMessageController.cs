using System;
using System.Collections.Generic;
using ScaleBridge.Message.Event;

using NLog;
using NServiceBus;
using System.Net.Http;
using System.Net;

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

		public void Post(JotformMessage x)
		{
			Logger.Info (x.FormID);
			Logger.Info (x.SubmissionID);
		}

    }
}
