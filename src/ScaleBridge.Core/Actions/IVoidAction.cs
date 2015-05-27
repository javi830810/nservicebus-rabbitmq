using System;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Event;

namespace ScaleBridge.Core
{
	public interface IVoidAction: IAction
	{
		void Execute (EventMessage input);
	}
}

