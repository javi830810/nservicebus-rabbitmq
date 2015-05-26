using System;
using NServiceBus;
using System.Collections.Generic;

namespace ScaleBridge.Message.Event
{
	[TimeToBeReceived("01:00:00")]
	public class JotformBaseMessage : IEvent
	{
		public long FormID { get; set; }
		public long SubmissionID { get; set; }
	}

	[TimeToBeReceived("01:00:00")]
	public class JotformMessage : IEvent
	{
		public long FormID { get; set; }
		public long SubmissionID { get; set; }
		public Dictionary<string,string> FormData { get; set; }
	}
}
