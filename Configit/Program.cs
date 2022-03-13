using System;
using System.Collections.Generic;
using System.IO;

namespace Resolve_dependencies
{
    class Program
    {
        static void Main(string[] args)
        {
            bool pass = true;

            string inputPath = @"/Users/husker/RiderProjects/Configit/input/";
            string outputPath = @"/Users/husker/RiderProjects/Configit/output/";
            var inputFiles = Directory.EnumerateFiles(inputPath, "input*.txt");
            foreach (string currentFile in inputFiles)
            {
                string outputFile = currentFile.Replace("input/input", "output/output");

                var lines = File.ReadAllLines(currentFile);
                int numPackages = int.Parse(lines[0]);
                
                Dictionary<string, string> packageToVersionDictionary = new Dictionary<string, string>();
                for (int j = 1; j <= numPackages; j++)
                {
                    var subs = lines[j].Split(',');

                    pass = packageToVersionDictionary.TryAdd(subs[0], subs[1]);
                    if (!pass) break;
                }

                if (pass)
                {
                    bool hasDependencies = lines.Length > 1 + numPackages;
                    if (hasDependencies)
                    {
                        for (int j = 2 + numPackages; j < lines.Length; j++)
                        {
                            if (!pass) break;
                            var subs = lines[j].Split(',');
                            int numDependencies = subs.Length / 2 - 1;

                            if (packageToVersionDictionary.ContainsKey(subs[0]))
                            {
                                if (packageToVersionDictionary[subs[0]] == subs[1])
                                {
                                    for (int k = 0; k < 2 * numDependencies; k += 2)
                                    {
                                        if (packageToVersionDictionary.ContainsKey(subs[k + 2]) &&
                                            packageToVersionDictionary[subs[k + 2]] == subs[k + 3])
                                            continue;
                                        pass = packageToVersionDictionary.TryAdd(subs[k + 2], subs[k + 3]);
                                    }
                                }
                            }
                        }
                    }
                }
                Directory.CreateDirectory(outputPath);
                File.WriteAllText(outputFile, pass ? "PASS" : "FAIL");
            }
        }
    }
}