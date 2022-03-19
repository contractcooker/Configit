using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Hosting;

namespace Configit
{
    public class BatchFileHelper : BackgroundService
    {
        
        public IEnumerable<string> InputFiles { get; set; }
        private readonly IIoHelper _iohelper;
        private readonly IFileHelper _fileHelper;
        private readonly IHost _host;

        public BatchFileHelper(IIoHelper ioHelper, IFileHelper fileHelper, IHost host)
        {
            _iohelper = ioHelper;
            _fileHelper = fileHelper;
            _host = host;
            InputFiles = _iohelper.GetInputFiles();
        }

        
        
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (string currentFile in InputFiles)
            {
                Guard.Against.NullOrEmpty(currentFile, nameof(currentFile));
                _iohelper.CreateOutputFile(currentFile, _fileHelper.Validate(File.ReadAllLines(currentFile)) ? "PASS" : "FAIL");
            }

            _host.StopAsync();
            return null;
        }
    }
}