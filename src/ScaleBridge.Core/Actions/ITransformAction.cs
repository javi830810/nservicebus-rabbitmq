using System;
using System.Collections.Generic;
using ScaleBridge.Message.Event;
using ScaleBridge.Message.Object;

namespace ScaleBridge.Core
{
	public interface ITransformAction: IAction
	{
		EventMessage Transform(EventMessage input);
	}
}

