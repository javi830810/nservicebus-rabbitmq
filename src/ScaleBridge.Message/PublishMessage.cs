using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace ScaleBridge.Message.Event
{
    public class PublishMessage: IEvent
    {
        public Dictionary<string,string> MessageData { get; set;}
    }
}
