using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mvc_Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            lock (program)
            {
                try
                {
                    CreateHostBuilder(args).Build().Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Исключение в Main(... args)\n"+ ex);
                }
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<IdentityAppStartup>();
                });
    }
}
