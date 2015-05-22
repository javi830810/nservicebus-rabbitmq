using System;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Command;
using NServiceBus;

namespace ScaleBridge.Core
{
	public class WebhookPublisherManager: IMessagePublisherManager
	{
		public IWebhookStore WebhookStore { get; set; }
		public IBus Bus { get; set; }

		public void Publish (MessageInfo message)
		{
			foreach(var webhook in WebhookStore.QueryByEventType(message.EventType))
			{
				Bus.Send ("ScaleBridge.Publisher", new SubmitViaHttpCommand () {
					Url = webhook.Url,
					Method = webhook.Method,
					MessageData = message.MessageData
				});
			}
		}
	}
}

