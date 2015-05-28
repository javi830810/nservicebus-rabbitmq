using System;
using System.Collections.Generic;
using NServiceBus;

namespace ScaleBridge.Message.Command
{
	public class SubmitViaHttpCommand:IMessage
	{
		public string Url { get; set; }
		public string Method { get; set; }
		public Dictionary<string,string> Headers { get; set; }
		public Dictionary<string,string> MessageData { get; set; }
	}
}

