using System;
using System.Collections.Generic;
using ScaleBridge.Message;
using ScaleBridge.Message.Object;

namespace ScaleBridge.Core
{
	public interface ITransformAction: IAction
	{
		EventMessage Transform(EventMessage input);
	}
}

