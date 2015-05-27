using System;
using System.Collections.Generic;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Event;

namespace ScaleBridge.Core
{
	public class MapTransformAction: ITransformAction
	{
		public MessageTemplate OutputMessageTemplate { get; set; }

		public EventMessage Transform(EventMessage input)
		{
			//No other Transformation happens at this level
			return OutputMessageTemplate.Map (input);
		}
	}
}