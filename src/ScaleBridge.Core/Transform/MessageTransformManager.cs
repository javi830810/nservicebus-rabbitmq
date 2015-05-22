using System;
using System.Collections.Generic;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Event;

namespace ScaleBridge.Core
{
	public class MessageTransformManager:IMessageTransformManager
	{
		public IEnumerable<MessageInfo> Transform(InputMessage message)
		{
			
			return new List<MessageInfo> () {
				new MessageInfo () {
					EventType = "UserLogged",
					MessageData = new Dictionary<string,string> () {
						{ "event1","transformed1" }
					}
				},
				new MessageInfo () {
					EventType = "UserPayment",
					MessageData = new Dictionary<string,string> () {
						{ "event2","transformed2" }
					}
				},
			};
		}
	}
}

