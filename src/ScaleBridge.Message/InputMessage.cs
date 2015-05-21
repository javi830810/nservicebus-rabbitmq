using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace ScaleBridge.Message.Event
{
    [TimeToBeReceived("01:00:00")]
    public class InputMessage: IEvent
    {
        public Dictionary<string,string> MessageData { get; set;}
    }
}
