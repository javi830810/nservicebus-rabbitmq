using System;

namespace ScaleBridge.Core
{
	public interface ISettings
	{
		string Get(string key);
	}
}