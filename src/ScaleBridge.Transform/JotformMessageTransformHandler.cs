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
	public class JotformMessageTransformHandler :
		IHandleMessages<JotformBaseMessage>,
		IHandleMessages<JotformMessage>
	{
		public IJotformMessageTransformManager JotformMessageTransformManager { get; set; }

		public IBus Bus { get; set; }

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
			Logger.Info(string.Format("FormID: {0}", message.FormID));
			Logger.Info(string.Format("SubmissionID: {0}", message.SubmissionID));

			foreach (var item in message.FormData) {
				Logger.Info (string.Format ("{0}: {1}", item.Key, item.Value));
			}

			using (StreamReader file = File.OpenText(@"fullcontact1.json"))
			{
				var serializer = new Newtonsoft.Json.JsonSerializer();
				var contactInfo = (FullContactApiMessage)serializer.Deserialize (file, typeof(FullContactApiMessage));

				Console.WriteLine (contactInfo);

				var bitrixMessage = new BitrixMessage () {
					JotFormData = message,
					FullContactData = contactInfo
				};

				Bus.SendLocal(bitrixMessage);
			}
		}

		public void Handle(JotformBaseMessage message)
		{
			Logger.Info("JotformBaseMessage received!!!");

			JotformMessageTransformManager.Transform(message);
		}
	}
}
