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
                //start at index 1 because that's where the packages start [0] just gives us the number of packages
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
                        //adding 2 to account for number of packages and number of dependencies.
                        for (int j = 2 + numPackages; j < lines.Length; j++)
                        {
                            if (!pass) break;
                            var subs = lines[j].Split(',');
                            int numDependencies = subs.Length / 2 - 1;

                            if (packageToVersionDictionary.ContainsKey(subs[0]))
                            {
                                if (packageToVersionDictionary[subs[0]] == subs[1])
                                {
                                    //since each dependency has 2 fields (name,version) multiply by two and iterate by 2
                                    for (int k = 0; k < 2 * numDependencies; k += 2)
                                    {
                                        //skip over first 2 places as they denote the dependent package and version, dependencies start at [2] and occur every 2 indices.
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