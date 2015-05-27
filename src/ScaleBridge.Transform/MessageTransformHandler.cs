using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaleBridge.Core;
using ScaleBridge.Message;
using ScaleBridge.Message.Event;
using ScaleBridge.Message.Object;
using NServiceBus;
using NLog;

namespace ScaleBridge.Transform
{
    public class MessageTransformHandler : 
	IHandleMessages<EventMessage> 
    {
		public IPipelineStore PipelineStore { get; set; }
		public Logger Logger { get; set; }
        
		public MessageTransformHandler()
        {
			Logger = LogManager.GetLogger(GetType().FullName);
        }
        
        public void Handle(EventMessage message)
        {
			Logger.Info("InputMessage Start");
            
			var pipeline = PipelineStore.GetPipelineForMessage (message);
			if (pipeline != null)
				pipeline.Execute (message);

			Logger.Info("InputMessage Completed");
        }
    }
}
