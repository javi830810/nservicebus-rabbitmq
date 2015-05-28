using System;
using ScaleBridge.Message.Object;
using ScaleBridge.Message;

namespace ScaleBridge.Core
{
	public interface IPipelineStore
	{

		ActionTreePipeline GetPipelineForMessage(EventMessage message);
	}
}

