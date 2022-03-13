using System.Collections.Generic;
using System.IO;

namespace Resolve_dependencies
{

    public class IoHelper
    {
        public string InputPath { get; set; } = @"/Users/husker/RiderProjects/Configit/input/";
        public string OutputPath { get; set; } = @"/Users/husker/RiderProjects/Configit/output/";


        public IEnumerable<string> GetInputFiles()
        {
            return Directory.EnumerateFiles(InputPath, "input*.txt");
        }

        public void CreateOutputFiles(bool pass)
        {
            var inputFiles = this.GetInputFiles();
            foreach (string currentFile in inputFiles)
            {
                string outputFile = currentFile.Replace("input/input", "output/output");
                Directory.CreateDirectory(this.OutputPath);
                File.WriteAllText(outputFile, pass ? "PASS" : "FAIL");
            }
        }
    }
}