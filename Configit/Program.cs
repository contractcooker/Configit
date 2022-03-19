using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Configit
{
    class Program
    {
        static void Main(string[] args)
        {
            //IIoHelper
            // IoHelper ioh = new IoHelper();
            CreateHostBuilder(args).Build().Run();
        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddHostedService<BatchFileHelper>()
                        .AddScoped<IIoHelper, IoHelper>().AddScoped<IFileHelper, FileHelper>());
    }
}