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

			var pipeLineStore = new MemoryPipelineStore () {
				Store = new Dictionary<string, ActionTreePipeline> () { { 
						"jotform", 
						new ActionTreePipeline () {
							Current = new APITransformAction () {
								Url = "api.jotform.com",
								Method = "POST",
								PostObjectMap = new DataMap () {
									PropertyMaps = new Dictionary<string, string> () {
										{ "SubmissionID", "submissionId" }
									}
								},
								OutputMessageTemplate = new MessageTemplate () {
									MessageType = "JotFormMessage",
									PropertyMaps = new Dictionary<string,string> () {
										{ "formID", "FormID" },
										{ "submissionID", "submissionID" }
									}
								}
							},
							Children = new List<ActionTreePipeline>(){
								new ActionTreePipeline(){
									Current = new WebhookPublishAction(){
										Bus = container.Resolve<IBus>(),
										WebhookStore = container.Resolve<IWebhookStore>(),
									}
								}

							}
						}
					}
				}
			};

			container.Register(Component.For<IPipelineStore>()
				.Instance(pipeLineStore)
				.LifestyleSingleton());
        }

    }
}