using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace ScaleBridge.Message.Object
{
	public class MessageInfo
    {
		public string EventType { get; set;}
        public Dictionary<string,string> MessageData { get; set;}
    }
}
