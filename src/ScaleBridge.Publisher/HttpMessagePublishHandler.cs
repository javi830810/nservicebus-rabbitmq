using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaleBridge.Core;
using ScaleBridge.Message;
using ScaleBridge.Message.Command;
using NServiceBus;
using NLog;
using RestSharp;

namespace ScaleBridge.Publisher
{
    public class HttpMessagePublishHandler : 
	IHandleMessages<SubmitViaHttpCommand> 
    {
        public Logger Logger { get; set; }
        
		public HttpMessagePublishHandler()
        {
			Logger = LogManager.GetLogger(GetType().FullName);
        }
        
		public void Handle(SubmitViaHttpCommand message)
        {
			Logger.Info("HttpMessage Start");
            
			try
			{
				Logger.Info("Message to: " + message.Url);
				var client = new RestClient(message.Url);
				var request = new RestRequest();
				request.Method = (message.Method == "POST") ? Method.POST : Method.GET;

				if(message.Headers != null)
					foreach (var keyValue in message.Headers) 
					{
						request.AddHeader(keyValue.Key, keyValue.Value);
					}

				if(message.MessageData != null)
					foreach (var keyValue in message.MessageData) 
					{
						request.AddParameter(keyValue.Key, keyValue.Value);
					}

				// execute the request
				IRestResponse response = client.Execute(request);
				var content = response.Content; // raw content as string

			}
			catch(Exception ex)
			{
				Logger.Error (ex.ToString ());
				throw ex;
			}


			Logger.Info("HttpMessage End");
        }
    }
}
