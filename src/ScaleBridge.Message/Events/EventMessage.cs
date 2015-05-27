using System;
using System.Collections.Generic;

namespace ScaleBridge.Message.Event
{
	public class EventMessage
	{
		public string MessageType { get; set;}
		public Dictionary<string,string> Data { get; set; }
	}
}

