using System;
using System.Collections.Generic;
using ScaleBridge.Message.Object;
using ScaleBridge.Message;

namespace ScaleBridge.Core
{
	public class ActionTreePipeline
	{
		public IAction Current { get; set; }

		public IEnumerable<ActionTreePipeline> Children { get; set; }

		public ActionTreePipeline ()
		{
		}

		public ActionTreePipeline (IAction current, IEnumerable<ActionTreePipeline> nextActions = null)
		{
			this.Current = current;
			this.Children = nextActions;
		}

		public void Execute(EventMessage input)
		{
			var currentInput = input;

			if (this.Current != null && Current is ITransformAction) 
			{
				currentInput = ((ITransformAction)Current).Transform(input);
			}
			else if(this.Current != null && Current is IVoidAction) 
				((IVoidAction)Current).Execute(input);

			//Executing Children Actions
			if(this.Children != null)
				foreach (var action in this.Children) 
				{
					action.Execute (currentInput);
				}
		}

	}
}

