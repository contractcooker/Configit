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

        public void CreateOutputFile(string currentFile, string output)
        {
            string outputFile = currentFile.Replace("input/input", "output/output");
            Directory.CreateDirectory(OutputPath);
            File.WriteAllText(outputFile, output);
        }
    }
}