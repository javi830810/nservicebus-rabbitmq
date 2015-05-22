using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaleBridge.Core;
using ScaleBridge.Message;
using ScaleBridge.Message.Event;
using NServiceBus;
using NLog;

namespace ScaleBridge.Transform
{
    public class MessageTransformHandler : 
        IHandleMessages<InputMessage> 
    {
        public IMessageTransformManager MessageTransformManager { get; set; }
		public IMessagePublisherManager MessagePublisher { get; set; }

        public Logger Logger { get; set; }
        
		public MessageTransformHandler()
        {
			Logger = LogManager.GetLogger(GetType().FullName);
        }
        
        public void Handle(InputMessage message)
        {
			Logger.Info("InputMessage Start");
            
			foreach (var messageInfo in MessageTransformManager.Transform(message)) 
			{

				MessagePublisher.Publish (messageInfo);
			}

			Logger.Info("InputMessage Completed");
        }
    }
}
