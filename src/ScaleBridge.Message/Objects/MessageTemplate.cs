using System;
using ScaleBridge.Message.Event;

namespace ScaleBridge.Message.Object
{
	public class MessageTemplate:DataMap
	{
		public string MessageType { get; set; }

		public EventMessage Map(EventMessage input)
		{
			return new EventMessage () {
				MessageType = this.MessageType,
				Data = base.Map (input.Data)
			};
		}
	}
}

