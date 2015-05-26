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

namespace Protocols
{
	public class MultiFormDataMediaTypeFormatter : FormUrlEncodedMediaTypeFormatter
	{
		private const string StringMultipartMediaType = "multipart/form-data";

		public MultiFormDataMediaTypeFormatter()
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

				var boundary = bodyContent.Substring(0, bodyContent.IndexOf("\n"));
				var bodyDictionary = ParseContent(bodyContent, boundary);

				var obj = Activator.CreateInstance(type);
				var propertiesFromObj = obj.GetType().GetRuntimeProperties().ToList();

				foreach (var property in propertiesFromObj)
				{	
					var formData = bodyDictionary.Keys.FirstOrDefault(x => x.ToLower() == property.Name.ToLower());

					if (formData == null) continue;

					try
					{
						var strValue = bodyDictionary[formData];
						var valueType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
						var value = Convert.ChangeType(strValue, valueType);
						property.SetValue(obj, value);
					}
					catch (Exception e)
					{
						formatterLogger.LogError (this.GetType().FullName ,e.ToString ());
					}
				}

				return obj;
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

