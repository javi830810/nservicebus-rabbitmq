using System;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Event;

namespace ScaleBridge.Core
{
	public interface IPipelineStore
	{
		ActionTreePipeline GetPipelineForMessage(EventMessage message);
	}
}

