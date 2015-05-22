using System;
using System.IO;
using System.Web;
using System.Configuration;
using Castle.MicroKernel.Registration;
using System.Web.Http;

using ScaleBridge.Core;
using ScaleBridge.Message;

namespace ScaleBridge.Web
{
	public class ControllersInstaller : IWindsorInstaller
	{
		public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}

			container.Register(Types.FromThisAssembly()
				.BasedOn<ApiController>()
				.If(t => t.Name.EndsWith("Controller", StringComparison.Ordinal))
				.Configure(c => c.LifestyleTransient()));
		}
	}
}

