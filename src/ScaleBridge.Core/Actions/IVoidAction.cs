using System;
using ScaleBridge.Message.Object;
using ScaleBridge.Message;

namespace ScaleBridge.Core
{
	public interface IVoidAction: IAction
	{
		void Execute (EventMessage input);
	}
}

