using System;
using System.Collections.Generic;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Event;

namespace ScaleBridge.Core
{
	public class MemoryPipelineStore: IPipelineStore
	{
		public Dictionary<string,ActionTreePipeline> Store { get; set; }

		public ActionTreePipeline GetPipelineForMessage(EventMessage message)
		{
			return Store [message.MessageType];
		}

	}
}

