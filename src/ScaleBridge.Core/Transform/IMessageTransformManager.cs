using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaleBridge.Message.Event;
using ScaleBridge.Message.Object;
using NServiceBus;

namespace ScaleBridge.Core
{
    public interface IMessageTransformManager
    {
		IEnumerable<MessageInfo> Transform(InputMessage message);
    }

    
}
