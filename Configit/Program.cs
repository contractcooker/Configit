using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace Resolve_dependencies
{
    class Program
    {
        static void Main(string[] args)
        {
            bool pass = true;
            IoHelper ioh = new IoHelper();
            Dictionary<string, string> packageToVersionDictionary;

            var inputFiles = ioh.GetInputFiles();
            foreach (string currentFile in inputFiles)
            {
                FileHelper fh = new FileHelper(currentFile);
                var packageList = fh.PackageList;
                try
                {
                    packageToVersionDictionary = packageList.ToDictionary(p => p.package, p => p.version);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e);
                    ioh.CreateOutputFile(currentFile, "FAIL");
                }



                // if (pass)
                // {
                //     bool hasDependencies = lines.Length > 1 + numPackages;
                //     if (hasDependencies)
                //     {
                //         //adding 2 to account for number of packages and number of dependencies.
                //         for (int j = 2 + numPackages; j < lines.Length; j++)
                //         {
                //             if (!pass) break;
                //             var subs = lines[j].Split(',');
                //             int numDependencies = subs.Length / 2 - 1;
                //
                //             if (packageToVersionDictionary.ContainsKey(subs[0]))
                //             {
                //                 if (packageToVersionDictionary[subs[0]] == subs[1])
                //                 {
                //                     //since each dependency has 2 fields (name,version) multiply by two and iterate by 2
                //                     for (int k = 0; k < 2 * numDependencies; k += 2)
                //                     {
                //                         //skip over first 2 places as they denote the dependent package and version, dependencies start at [2] and occur every 2 indices.
                //                         if (packageToVersionDictionary.ContainsKey(subs[k + 2]) &&
                //                             packageToVersionDictionary[subs[k + 2]] == subs[k + 3])
                //                             continue;
                //                         pass = packageToVersionDictionary.TryAdd(subs[k + 2], subs[k + 3]);
                //                     }
                //                 }
                //             }
                //         }
                //     }
                // }
            }
        }
    }
}