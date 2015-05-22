using System.Collections.Generic;
using System.IO;
using System.Web;
using Castle.MicroKernel.Registration;
using System;
using System.Configuration;
using ScaleBridge.Core;
using ScaleBridge.Message.Object;

namespace ScaleBridge.Transform
{
    //@bweller
    //This is one of the constructions of CastleWindsor
    //To inject dependencies
    public class ManagersInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            
            
//            container.Register(AllTypes.FromAssemblyContaining(typeof(IMessageTransformManager))
//                .Pick()
//                .If(Component.IsInNamespace("ScaleBridge.Core"))
//                .If(t => t.Name.EndsWith("Manager", StringComparison.Ordinal))
//                .WithService.DefaultInterfaces()
//                .Configure(c => c.LifestyleSingleton()));
			container.Register (
				Component.For<IMessagePublisherManager> ().ImplementedBy<WebhookPublisherManager> (),
				Component.For<IMessageTransformManager> ().ImplementedBy<MessageTransformManager> ()
			);

			var webhookStore = new MemoryWebhookStore(new List<Webhook>(){
				new Webhook(){
					Url = "http://iwanttodemo.appspot.com",
					Method = "POST",
					EventType = "UserLogged"
				},
				new Webhook(){
					Url = "http://iwanttodemo.appspot.com",
					Method = "POST",
					EventType = "UserPayment"
				}
			});


			container.Register(Component.For<IWebhookStore>()
				.Instance(webhookStore)
				.LifestyleSingleton());

        }

    }
}