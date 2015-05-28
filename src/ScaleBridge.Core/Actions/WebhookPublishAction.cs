using System;
using System.Collections.Generic;
using ScaleBridge.Message.Object;
using ScaleBridge.Message;
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
				var postData = (input.Data != null) ? input.Data : new Dictionary<string,string> ();

				if(webhook.DefaultPostData != null)
					postData.Merge(webhook.DefaultPostData);

				Bus.Send ("ScaleBridge.Publisher", new SubmitViaHttpCommand () {
					Url = webhook.Url,
					Method = webhook.Method,
					Headers = webhook.Headers,
					MessageData = postData
				});
			}
		}
	}


}

	