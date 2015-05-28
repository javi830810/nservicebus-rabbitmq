using System;
using System.Collections.Generic;

namespace ScaleBridge.Message.Object
{
	public class DataMap 
	{
		public Dictionary<string,string> DefaultValues { get; set; }
		public Dictionary<string,string> PropertyMaps { get; set; }

		public bool ExistsMap(string property)
		{
			return PropertyMaps.ContainsKey (property);
		}

		public string GetMappedProperty(string property)
		{
			return PropertyMaps[property];
		}

		public virtual Dictionary<string,string> Map(Dictionary<string,string> input)
		{
			var result = new Dictionary<string,string> ();
		
			foreach(var keyValue in input)
			{
				if(this.ExistsMap(keyValue.Key))
					result[this.GetMappedProperty(keyValue.Key)] = input[keyValue.Key];
			}

			result.Merge(DefaultValues);

			return result;
		}
	}
}

