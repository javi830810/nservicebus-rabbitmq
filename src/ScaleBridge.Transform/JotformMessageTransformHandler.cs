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

		/// <summary>
		/// Handles a JotformMessage.  This contains the full form submission data.
		/// </summary>
		/// <param name="message">The message to handle.</param>
		/// <remarks>This method will be called when a message arrives on the bus and should contain
		///  the custom logic to execute when the message is received.</remarks>
		public void Handle (JotformMessage message)
		{
			// TODO: Lookup contact information from api.fullcontact.com.


			Logger.Info("JotformMessage received!!!");
		}

		public void Handle(JotformBaseMessage message)
		{
			Logger.Info("JotformBaseMessage received!!!");

			JotformMessageTransformManager.Transform(message);
		}
	}
}
