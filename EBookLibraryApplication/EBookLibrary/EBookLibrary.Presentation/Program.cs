using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace EBookLibrary.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging((hostingContext, logging) => {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("logging"));
                        logging.AddConsole(); // important for development
                        logging.AddDebug(); // important for hosting environment
                        logging.AddNLog(); // important to log to file
                    }).UseStartup<Startup>();
                });
    }
}
