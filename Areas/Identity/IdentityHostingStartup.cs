using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Mvc_Auth.Data;

[assembly: HostingStartup(typeof(Mvc_Auth.Areas.Identity.IdentityHostingStartup))]
namespace Mvc_Auth.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        private ILogger<IdentityHostingStartup> _logger { get; set; }
        public IdentityHostingStartup()
        {
            _logger = LoggerFactory
                .Create(options => options.AddConsole())
                .CreateLogger<IdentityHostingStartup>();
        }

        public void Configure(IWebHostBuilder builder)
        {

            _logger.LogInformation($"Configure( ... )");
            builder.ConfigureServices((context, services) => {
                foreach (var kv in context.Configuration.AsEnumerable())                
                    Console.WriteLine($"\t{kv.Key}={kv.Value}");
                

            });
        }
    }
}