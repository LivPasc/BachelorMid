using System.Threading;
using BachelorMid.Controllers;
using BachelorMid.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace BachelorMid
{
    public class Program
    {
        private static DataAccessContext context;
        private static ILogger<DatabaseController> logger;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            DatabaseController cont = new DatabaseController(logger, context);

            Thread thread = new Thread(new ThreadStart(cont.UpdateDatabase))
            {
                IsBackground = true,
                Name = "Data Polling Thread",
            };
            thread.Start();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
