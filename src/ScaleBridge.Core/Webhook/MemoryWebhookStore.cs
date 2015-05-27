using System;
using System.Linq;
using System.Collections.Generic;
using ScaleBridge.Message.Object;

namespace ScaleBridge.Core
{
	public class MemoryWebhookStore:IWebhookStore
	{
		private List<Webhook> webhooks;

		public MemoryWebhookStore(List<Webhook>webhooks)
		{
			this.webhooks = webhooks;
		}

		public IEnumerable<Webhook> QueryByEventType(string eventType)
		{

			return this.webhooks.FindAll (x => x.EventType == eventType);
		}
	}
}

