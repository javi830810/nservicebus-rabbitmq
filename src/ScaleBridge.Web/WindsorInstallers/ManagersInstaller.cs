﻿using System.IO;
using System.Web;
using Castle.MicroKernel.Registration;
using System;
using System.Configuration;
using ScaleBridge.Core;
using ScaleBridge.Message;

namespace ScaleBridge.Web
{
    public class ManagersInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            var Settings = container.Resolve<Settings>();

            container.Register(AllTypes.FromAssemblyContaining(typeof(IMessageTransformManager))
                .Pick()
                .If(Component.IsInNamespace("ScaleBridge.Core"))
                .If(t => t.Name.EndsWith("Manager", StringComparison.Ordinal))
                .WithService.DefaultInterfaces()
                .Configure(c => c.LifestyleTransient()));

        }
    }
}
