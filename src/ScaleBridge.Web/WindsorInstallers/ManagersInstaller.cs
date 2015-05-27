using System;
using Castle.MicroKernel.Registration;
using ScaleBridge.Core;

namespace ScaleBridge.Web
{
	public class ManagersInstaller : IWindsorInstaller
	{
		public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			if (container == null)
				throw new ArgumentNullException("container");


		}
	}
}

