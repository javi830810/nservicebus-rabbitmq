using System;
using ScaleBridge.Core;
using ScaleBridge.Message;
using ScaleBridge.Message.Event;
using NServiceBus;
using NLog;

namespace ScaleBridge.Transform
{
	public class JotformMessageTransformHandler :
		IHandleMessages<JotformBaseMessage>,
		IHandleMessages<JotformMessage>
	{
		public IJotformMessageTransformManager JotformMessageTransformManager { get; set; }
		public Logger Logger { get; set; }

		public JotformMessageTransformHandler()
		{
			Logger = LogManager.GetLogger(GetType().FullName);
		}

		public void Handle (JotformMessage message)
		{
			Logger.Info("JotformMessage received!!!");
		}

		public void Handle(JotformBaseMessage message)
		{
			Logger.Info("JotformBaseMessage received!!!");

			JotformMessageTransformManager.Transform(message);
		}
	}
}

