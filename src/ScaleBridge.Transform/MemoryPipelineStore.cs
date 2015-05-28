using System;
using System.Collections.Generic;
using ScaleBridge.Message.Object;
using ScaleBridge.Message;
using ScaleBridge.Core;
using NServiceBus;
using Castle.Windsor;
using JF = ScaleBridge.Partner.JotForm;

namespace ScaleBridge.Transform
{
	public class MemoryPipelineStore: IPipelineStore
	{
		IWindsorContainer container;
		public MemoryPipelineStore(IWindsorContainer container)
		{
			this.container = container;

		}

		public ActionTreePipeline GetPipelineForMessage(EventMessage message)
		{
			return new ActionTreePipeline () {
				Current = new JF.JotFormAPIAction () { },
				Children = new List<ActionTreePipeline> () {
					new ActionTreePipeline () {
						Current = new MapTransformAction () {
							OutputMessageTemplate = new MessageTemplate(){
								MessageType = "BitrixAPIPublish",
								DefaultValues = new Dictionary<string,string>(){
									{ "TITLE", "WHATEVER"},
								}, 
								PropertyMaps = new Dictionary<string,string>(){
									{"userType", "UF_CRM_1428691203"},
									{"emailAddress", "EMAIL_WORK"},
									{ "challengeTitle", "UF_CRM_1428691018"},
								}
							}
						},
						Children = new List<ActionTreePipeline>(){
							new ActionTreePipeline () {
								Current = new WebhookPublishAction () {
									Bus = this.container.Resolve<IBus> (),
									WebhookStore = container.Resolve<IWebhookStore> (),
								}
							}
						}
					}

				}
			};
		}

	}
}

