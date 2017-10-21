using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

using System.Collections.Generic;
using System.Fabric;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
namespace Web
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class Web: StatelessService
    {
        public Web(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                new HttpSysCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>

                new WebHostBuilder()
                        .UseHttpSys()
                        .ConfigureLogging( (ctx,logging)=>{
                            logging.AddConsole();
                            logging.AddDebug();
                        })
                        .ConfigureServices(
                            services => services
                        .AddSingleton<StatelessServiceContext>(serviceContext))
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                        .UseStartup<Startup>()
                        .UseUrls(url)

                        .Build()))
            };

        }
    }
}
