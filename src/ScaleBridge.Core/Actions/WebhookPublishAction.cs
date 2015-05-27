using System;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Event;
using ScaleBridge.Message.Command;
using NServiceBus;

namespace ScaleBridge.Core
{
	public class WebhookPublishAction: IVoidAction
	{
		public IWebhookStore WebhookStore { get; set; }
		public IBus Bus { get; set; }

		public void Execute (EventMessage input)
		{
			foreach(var webhook in WebhookStore.QueryByEventType(input.MessageType))
			{
				Bus.Send ("ScaleBridge.Publisher", new SubmitViaHttpCommand () {
					Url = webhook.Url,
					Method = webhook.Method,
					MessageData = input.Data
				});
			}
		}
	}
}

