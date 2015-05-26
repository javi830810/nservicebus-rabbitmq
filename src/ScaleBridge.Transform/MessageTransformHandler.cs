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
        public Logger Logger { get; set; }
        
        public MessageTransformHandler()
        {
			Logger = LogManager.GetLogger(GetType().FullName);
        }
        
        public void Handle(InputMessage message)
        {
			Logger.Info("InputMessage received!!!");
            
            foreach(var x in message.MessageData){
				Logger.Info("Key: " + x.Key);
				Logger.Info("Value: " + x.Value);
            }
            
            MessageTransformManager.Transform(message);
        }
    }
    
    public class  HttpPublishHandler: 
        IHandleMessages<PublishMessage> 
    {
        public IMessageTransformManager MessageTransformManager { get; set; }
		public Logger Logger { get; set; }
        
        public HttpPublishHandler()
        {
			Logger = LogManager.GetLogger(GetType().FullName);
        }
        
        public void Handle(PublishMessage message)
        {
            Logger.Info("Publish MEssage Received");
            
            foreach(var x in message.MessageData){
                Logger.Info("Key: " + x.Key);
                Logger.Info("Value: " + x.Value);
            }
            
            //HTTP the message to the given webhooks
            
            Logger.Info("HTTP the message to the given webhooks");
        }
    }
}
