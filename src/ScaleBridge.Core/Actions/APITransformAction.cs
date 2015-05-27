using System;
using System.Collections.Generic;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Event;
using RestSharp;
using Newtonsoft.Json;

namespace ScaleBridge.Core
{
	/// <summary>
	/// This class will take an input Message and
	/// use it as a Post Input to an API, 
	/// from there it will get the result and convert it into the Output Message
	/// </summary>
	public class APITransformAction: ITransformAction
	{
		public string Url { get; set; }
		public string Method { get; set; }
		public DataMap PostObjectMap { get; set; }
		public MessageTemplate OutputMessageTemplate { get; set; }

		public APITransformAction ()
		{
		}

		public EventMessage Transform (EventMessage input)
		{
			var apiResult = ExecuteAPICall (input);

			//We obtain the result from The API Call and Transform the result instead
			return OutputMessageTemplate.Map(new EventMessage(){ Data = apiResult });
		}

		protected virtual Dictionary<string,string> ExecuteAPICall(EventMessage input)
		{
			var client = new RestClient(Url);
			var request = new RestRequest();
			request.Method = (Method == "POST") ? RestSharp.Method.POST : RestSharp.Method.GET;

			if(PostObjectMap != null)
				foreach (var keyValue in PostObjectMap.Map(input.Data)) 
				{
					request.AddParameter(keyValue.Key, keyValue.Value);
				}

			// execute the request
			IRestResponse response = client.Execute(request);

			return JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
		}

	}
}

