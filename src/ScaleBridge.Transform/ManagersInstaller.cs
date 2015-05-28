using System.Collections.Generic;
using System.IO;
using System.Web;
using Castle.MicroKernel.Registration;
using System;
using System.Configuration;
using ScaleBridge.Core;
using ScaleBridge.Message.Object;
using NServiceBus;
using JF = ScaleBridge.Partner.JotForm;

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
            

			var webhookStore = new MemoryWebhookStore(new List<Webhook>(){
				new Webhook(){
					Url = "https://scalebridge.bitrix24.com/crm/configs/import/lead.php",
					Method = "POST",
					EventType = JF.JotFormAPIAction.OutputMessageType,
					DefaultPostData = new Dictionary<string,string>(){
						{"LOGIN", "brian@scalebridge.net"},
						{"PASSWORD", "zodxdkfxhmuwsbnu"},
						{"ASSIGNED_BY_ID", "2"},
						{"SOURCE_ID", "WEB"}
					}
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