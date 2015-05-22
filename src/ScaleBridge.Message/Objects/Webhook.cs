using System;

namespace ScaleBridge.Message.Object
{
	public class Webhook
	{
		public string EventType { get; set; }
		public string Url { get; set; }
		public string Method { get; set; }

	}
}

