using System;
using System.Threading;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;

using NServiceBus;

using NLog;
using NLog.Targets;
using NLog.Config;

using ScaleBridge.Message;
using ScaleBridge.Message.Event;

namespace ScaleBridge.Transform
{
    public class Program
    {
		public static void Main(string[] args)
        {
            
            var container = ConfigureContainer();
            
            ConfigureLogging(container);
            var configuration = ConfigureNSB(container);
            
            using (IStartableBus startableBus = Bus.Create(configuration))
            {
                IBus bus = startableBus.Start();
                
                //We will keep the process running using a while(true)
                //This is in order to not mess with standard input
                //Process can be started or killed by Ctrl+C
                while(true){
                    Thread.Sleep(6000);
                }
            }
        }

		private static IWindsorContainer ConfigureContainer()
        {
            var container = new WindsorContainer();
            
            container.Register(
                Component.For<Settings>()
                        .ImplementedBy<Settings>()
            );
            
            container.Install(
                new ManagersInstaller()
            );

            return container;
        }

		private static BusConfiguration ConfigureNSB(IWindsorContainer container)
        {
            var configuration = new BusConfiguration();
            var Settings = container.Resolve<Settings>();
            
            var conventionsBuilder = configuration.Conventions();

            conventionsBuilder.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Bus") && t.Namespace.EndsWith("Command"));
            conventionsBuilder.DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Bus") && t.Namespace.EndsWith("Event"));

            configuration.EndpointName("ScaleBridge.Transform");
            configuration.UseSerialization<JsonSerializer>();
            configuration.AssembliesToScan(
                AllAssemblies.Matching("ScaleBridge.Message")
                .And("NServiceBus")
                .And("ScaleBridge.Transform")
                .And("Newtonsoft.Json"));
			configuration.UseTransport<RabbitMQTransport>().ConnectionString(Settings.Get("rabbitmq"));
            configuration.DisableFeature<NServiceBus.Features.TimeoutManager>();
            configuration.Transactions().Disable();

            configuration.UsePersistence<InMemoryPersistence>();

            configuration.EnableInstallers();

            // Castle with a container instance
            configuration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

            return configuration;

        }
        
		private static void ConfigureLogging(IWindsorContainer container)
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
