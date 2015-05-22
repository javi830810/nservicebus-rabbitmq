using System;
using ScaleBridge.Message.Event;
using ScaleBridge.Message.Object;

namespace ScaleBridge.Core
{
	public interface IMessagePublisherManager
	{
		void Publish(MessageInfo message);
	}
}

