using System;
using NServiceBus;

namespace ScaleBridge.Message.Event
{
	public class BitrixMessage : IEvent
	{
		public JotformMessage JotFormData { get; set; }
		public FullContactApiMessage FullContactData { get; set; }
	}

	public class BitrixApiMessage : IEvent
	{

	}

}

