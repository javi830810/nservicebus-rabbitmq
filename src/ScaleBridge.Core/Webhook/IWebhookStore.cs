using System;
using System.Collections.Generic;
using ScaleBridge.Message.Object;

namespace ScaleBridge.Core
{
	public interface IWebhookStore
	{
		IEnumerable<Webhook> QueryByEventType(string eventType);
	}
}

