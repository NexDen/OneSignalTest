
using Microsoft.AspNetCore;
using Microsoft.OpenApi.Models;

namespace OneSignalTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options =>
                {
                    options.ListenAnyIP(7028);
                    // options.ListenAnyIP(7028, listenOptions =>
                    // {
                    //     listenOptions.UseHttps();
                    // });
                })
                .UseStartup<Startup>();

    }
}

