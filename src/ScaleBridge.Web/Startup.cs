using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using Microsoft.AspNet.Authorization;

using Microsoft.Framework.Runtime;
using Microsoft.Framework.ConfigurationModel.Json;
using Microsoft.Framework.ConfigurationModel;
using ILogger = Microsoft.Framework.Logging.ILogger;
using DI = Microsoft.Framework.DependencyInjection;
using ScaleBridge.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.Lifestyle;

using NServiceBus;
using NServiceBus.Config.ConfigurationSource;
using NServiceBus.Config;

using NLog;
using NLog.Targets;
using NLog.Config;

namespace ScaleBridge.Web
{
    public class Startup
    {
        private WindsorContainer container;
        private IHostingEnvironment environment;
        
        public Startup(IHostingEnvironment env)
        {
            this.environment = env;
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(DI.IServiceCollection services)
        {   
            services.AddMvc();
            services.AddOptions();

            //Please do not remove this line, it breaks Injection if not included
            services.Configure<AuthorizationOptions>(options =>
            {
            });

            ConfigureContainer(services);
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            ConfigureLogging(container);
            
            app.UseDefaultFiles();
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();
            
            //To resolve index.html-like files
            app.UseDefaultFiles();
            
            // Add MVC to the request pipeline.
            app.UseMvc();

            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");   
            
            app.ApplicationServices =  container.Resolve<IServiceProvider>();

            ConfigureBus();
        }
        
        private void ConfigureContainer(DI.IServiceCollection services)
        {
            this.container = new WindsorContainer();
            
             this.container.Register(
                Component.For<IHostingEnvironment>()
                        .Instance(this.environment)
            );
            
            container.Populate(services);

            this.container.Register(
                Component.For<Settings>()
                        .ImplementedBy<Settings>()
            );

            this.container.Install(
                new ManagersInstaller()
            );
            
            
            //Huge Patch do not remove until we find out what's happenning here
            container.Register(
                Component.For(typeof(IEnumerable<Microsoft.AspNet.Mvc.Core.IActionDescriptorProvider>))
                        .ImplementedBy(typeof(List<Microsoft.AspNet.Mvc.Core.IActionDescriptorProvider>))
            );

            container.BeginScope();
        }

        private void ConfigureBus()
        {
            var configuration = new BusConfiguration();
            var conventionsBuilder = configuration.Conventions();
            var Settings = container.Resolve<Settings>();

            conventionsBuilder.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Bus") && t.Namespace.EndsWith("Command"));
            conventionsBuilder.DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Bus") && t.Namespace.EndsWith("Event"));

            configuration.EndpointName("ScaleBridge.Web");
            configuration.UseSerialization<JsonSerializer>();
            configuration.AssembliesToScan(AllAssemblies.Matching("ScaleBridge.Message").And("NServiceBus"));
            configuration.UseTransport<RabbitMQTransport>().ConnectionString(Settings.Get("rabbitmq"));
            configuration.Transactions().Disable();

            configuration.UsePersistence<InMemoryPersistence>();

            configuration.EnableInstallers();
            
            // Castle with a container instance
            configuration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(this.container));

            var bus = Bus.Create(configuration);
            bus.Start();

        }
        
        private void ConfigureLogging(IWindsorContainer container)
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
            
            var factory = container.Resolve<ILoggerFactory>();
            factory.AddNLog(new NLog.LogFactory(config));
        }

    }

    

}
