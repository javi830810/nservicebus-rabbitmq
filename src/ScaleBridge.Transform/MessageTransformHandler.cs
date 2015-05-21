using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaleBridge.Core;
using ScaleBridge.Message;
using ScaleBridge.Message.Event;
using Microsoft.Framework.Logging;
using NServiceBus;

namespace ScaleBridge.Transform
{
    public class MessageTransformHandler : 
        IHandleMessages<InputMessage> 
    {
        public IMessageTransformManager MessageTransformManager { get; set; }
        public ILogger Logger { get; set; }
        
        public MessageTransformHandler(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(typeof(MessageTransformHandler).FullName);
        }
        
        public void Handle(InputMessage message)
        {
            Logger.LogInformation("InputMessage received!!!");
            
            foreach(var x in message.MessageData){
                Logger.LogInformation("Key: " + x.Key);
                Logger.LogInformation("Value: " + x.Value);
            }
            
            MessageTransformManager.Transform(message);
        }
    }
    
    public class  HttpPublishHandler: 
        IHandleMessages<PublishMessage> 
    {
        public IMessageTransformManager MessageTransformManager { get; set; }
        public ILogger Logger { get; set; }
        
        public HttpPublishHandler(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(typeof(MessageTransformHandler).FullName);
        }
        
        public void Handle(PublishMessage message)
        {
            Logger.LogInformation("Publish MEssage Received");
            
            foreach(var x in message.MessageData){
                Logger.LogInformation("Key: " + x.Key);
                Logger.LogInformation("Value: " + x.Value);
            }
            
            //HTTP the message to the given webhooks
            
            Logger.LogInformation("HTTP the message to the given webhooks");
        }
    }
}
