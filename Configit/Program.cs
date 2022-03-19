using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Configit
{
    class Program
    {
        static void Main(string[] args)
        {
            //IIoHelper
            IoHelper ioh = new IoHelper();
            var inputFiles = ioh.GetInputFiles();
            foreach (string currentFile in inputFiles)
            {
                FileHelper fh = new FileHelper(currentFile);
                ioh.CreateOutputFile(currentFile, fh.ValidConfig ? "PASS" : "FAIL");
            }
        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddHostedService<FileHelper>()
                        .AddScoped<IIoHelper, IoHelper>());
    }
}