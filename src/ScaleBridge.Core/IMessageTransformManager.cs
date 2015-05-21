using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaleBridge.Message.Event;
using NServiceBus;

namespace ScaleBridge.Core
{
    public interface IMessageTransformManager
    {
        void Transform(InputMessage message);
    }

    public class MessageTransformManager:IMessageTransformManager
    {
        //@bweller
        //Injected Bus!!!
        public IBus Bus { get; set; }

        public void Transform(InputMessage message)
        {
            Bus.SendLocal(new PublishMessage(){
                MessageData = new Dictionary<string,string>()
                {
                    {"data","transformed"}
                }
            });
            
            Bus.SendLocal(new PublishMessage(){
                MessageData = new Dictionary<string,string>()
                {
                    {"data2","transformed2"}
                }
            });
        }
    }
}
