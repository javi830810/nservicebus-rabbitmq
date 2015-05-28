using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;

namespace ScaleBridge.Web
{
	public class DictionaryMultiFormDataMediaTypeFormatter : FormUrlEncodedMediaTypeFormatter
	{
		private const string StringMultipartMediaType = "multipart/form-data";

		public DictionaryMultiFormDataMediaTypeFormatter()
		{
			this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(StringMultipartMediaType));
		}

		public override bool CanReadType(Type type)
		{
			return true;
		}

		public override bool CanWriteType(Type type)
		{
			return false;
		}

		private int GetBoundaryIndex(string body)
		{
			var index = body.IndexOf("\n");
			if (index >= 0)
				return index;
			return body.IndexOf("\r\n");
		}

		public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			try
			{

				string bodyContent;
					using (var reader = new StreamReader(readStream, Encoding.UTF8))
				{
					bodyContent = reader.ReadToEnd();
					// Do something with the value
				}

				var boundaryIndex = GetBoundaryIndex(bodyContent);

				if(boundaryIndex < 0)
					throw new Exception("Malformed Multipart request");

				var boundary = bodyContent.Substring(0, boundaryIndex);
				var bodyDictionary = ParseContent(bodyContent, boundary);

				var result = new Dictionary<string,string>();

				foreach (var property in bodyDictionary.Keys)
				{	
					try
					{
						var strValue = bodyDictionary[property];
						result[property] = strValue;
					}
					catch (Exception e)
					{
						formatterLogger.LogError (this.GetType().FullName ,e.ToString ());
					}
				}

				return result;
			}
			catch (Exception e)
			{
				formatterLogger.LogError (this.GetType().FullName ,e.ToString ());
				throw e;
			}
		}

		public Dictionary<string, string> ParseContent(string content, string boundary)
		{
			string[] list = content.Split(new string[] { boundary }, StringSplitOptions.RemoveEmptyEntries);
			string name="", val="";
			Dictionary<string, string> temp = new Dictionary<string, string>();

			foreach (String s in list)
			{
				if (s == "--" || s == "--\n")
				{
					//Do nothing.
				}
				else
				{
					string[] token = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
					val = "";
					name = "";
					foreach (string x in token)
					{

						if(x.StartsWith("Content-Disposition"))
						{
							//Name
							name = x.Substring(x.IndexOf("name=")+5, x.Length - x.IndexOf("name=")-5);
							name = name.Replace(@"\","");
							name = name.Replace("\"","");
						}
						if (x.StartsWith("--"))
						{
							break;
						}
						if (!x.StartsWith("--") && !x.StartsWith("Content-Disposition"))
						{
							val = x;
						}

					}
					if (name.Length > 0)
					{
						temp [name] = val;
					}
				}

			}
			return temp;        
		}

	}
}

