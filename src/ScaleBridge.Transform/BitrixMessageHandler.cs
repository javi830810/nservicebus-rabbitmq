using System;
using ScaleBridge.Core;
using ScaleBridge.Message;
using ScaleBridge.Message.Event;
using NServiceBus;
using NLog;
using System.IO;
using System.Collections.Generic;

namespace ScaleBridge.Transform
{
	public class BitrixMessageTransformHandler :
		IHandleMessages<BitrixMessage>
	{
		public IBitrixMessageTransformManager BitrixMessageTransformManager { get; set; }
		public Logger Logger { get; set; }

		public BitrixMessageTransformHandler()
		{
			Logger = LogManager.GetLogger(GetType().FullName);
		}

		public void Handle (BitrixMessage message)
		{
			// TODO: Lookup contact information from api.fullcontact.com.
			Logger.Info("BitrixMessage received!!!");
			Logger.Info(string.Format("message.JotFormData.FormID: {0}", message.JotFormData.FormID));
			Logger.Info(string.Format("message.JotFormData.SubmissionID: {0}", message.JotFormData.SubmissionID));

		}

	}
}
