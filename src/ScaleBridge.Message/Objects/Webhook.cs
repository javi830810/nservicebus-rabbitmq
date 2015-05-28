using System;
using System.Collections.Generic;

namespace ScaleBridge.Message.Object
{
	public class Webhook
	{
		public string EventType { get; set; }
		public string Url { get; set; }
		public string Method { get; set; }

		public Dictionary<string,string> Headers { get; set; }
		public Dictionary<string,string> DefaultPostData { get; set; }
	}
}

