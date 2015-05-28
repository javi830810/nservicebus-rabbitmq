using System.Collections.Generic;
using System.IO;
using System.Web;
using Castle.MicroKernel.Registration;
using System;
using System.Configuration;
using ScaleBridge.Core;
using ScaleBridge.Message.Object;
using NServiceBus;


namespace ScaleBridge.Transform
{
	public class PipelineStoreInstaller: IWindsorInstaller
	{
		public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			container.Register(Component.For<IPipelineStore>()
				.Instance(new MemoryPipelineStore (container))
				.LifestyleSingleton());
		}
	}
}

