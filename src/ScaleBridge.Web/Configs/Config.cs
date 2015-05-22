using System;
using System.Configuration;
using ScaleBridge.Core;

namespace ScaleBridge.Web
{
	public class Settings:ISettings
	{
        
        public Settings() {
            
        }

		public string Get(string key)
		{
			return System.Configuration.ConfigurationManager.AppSettings [key];
		}
	}
}