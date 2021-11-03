using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace VaccReg
{
    public class Program
    {
        public static string[] Args { get; set; }

        public static void Main(string[] args)
        {
            Args = args;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
