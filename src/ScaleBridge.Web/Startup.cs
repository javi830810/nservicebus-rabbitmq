using System;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using Protocols.Configs;
using System.Collections.Generic;
using System.Web.Http.Dispatcher;
using Protocols;
using Castle.Windsor;


using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;

using NLog;
using NLog.Config;
using NLog.Targets;
using NServiceBus;

using ScaleBridge.Core;

namespace ScaleBridge.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Run((ctx=> ctx.Response.WriteAsync("This is a test")));
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            app.UseWebApi(config);

            var container = ConfigureContainer ();
			ConfigureLogging();
			ConfigureBus (container);

			config.Services.Replace(
				typeof(IHttpControllerActivator),
				new ControllersActivator(container));

			config.Formatters.Add(new DictionaryMultiFormDataMediaTypeFormatter());
        }

		private IWindsorContainer ConfigureContainer()
		{
			var container = new WindsorContainer();

			container.Register(
				Component.For<ISettings>()
				.ImplementedBy<Settings>()
			);

			container.Install(
				new ManagersInstaller(),
				new ControllersInstaller()
			);

			return container;
		}

		private void ConfigureBus(IWindsorContainer container)
		{
			var configuration = new BusConfiguration();
			var conventionsBuilder = configuration.Conventions();
			var Settings = container.Resolve<ISettings>();

			conventionsBuilder.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Command"));
			conventionsBuilder.DefiningEventsAs(t => t.Namespace != null &&  t.Namespace.EndsWith("Event"));
			conventionsBuilder.DefiningMessagesAs(t => t.Namespace != null &&  t.Namespace.EndsWith("Message"));

			configuration.EndpointName("ScaleBridge.Web");
			configuration.UseSerialization<JsonSerializer>();
			configuration.AssembliesToScan(AllAssemblies.Matching("ScaleBridge.Message").And("NServiceBus"));
			configuration.UseTransport<RabbitMQTransport>().ConnectionString(Settings.Get("rabbitmq"));
			configuration.Transactions().Disable();

			configuration.UsePersistence<InMemoryPersistence>();

			configuration.EnableInstallers();

			// Castle with a container instance
			configuration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

			var bus = Bus.Create(configuration);
			bus.Start();
		}


		private void ConfigureLogging()
		{
			LoggingConfiguration config = new LoggingConfiguration();
			ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget
			{
				Layout = "${level}|${logger}|${message}${onexception:${newline}${exception:format=tostring}}"
			};
			config.AddTarget("console", consoleTarget);
			config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Info, consoleTarget));

			LogManager.Configuration = config;
			NServiceBus.Logging.LogManager.Use<NLogFactory>();

		}

    }
}
