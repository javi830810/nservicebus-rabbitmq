using System;
using System.Collections.Generic;
using NServiceBus;

namespace ScaleBridge.Message
{
	public class EventMessage
	{
		public string MessageType { get; set;}
		public Dictionary<string,string> Data { get; set; }
	}
}

