using System;
using System.Collections.Generic;
using System.IO;

namespace Resolve_dependencies
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            string filepath = @"/Users/husker/Downloads/testdata/";
            string filename;
            bool pass = true;
            for (int i = 0; i < 10; i++)
            {
                filename = filepath + "input00" + i + ".txt";
                // filename = filepath + "input009.txt";
                Console.WriteLine(filename);
                
                var lines = File.ReadAllLines(filename);
                int numPackages = int.Parse(lines[0]);

                Console.WriteLine($"Number of packages: {numPackages}");

                Dictionary<string, string> packageToVersionDictionary = new Dictionary<string, string>();
                for (int j = 1; j <= numPackages; j++)
                {
                    var subs = lines[j].Split(',');

                    pass = packageToVersionDictionary.TryAdd(subs[0],subs[1]);
                    if (!pass) break;

                }
                
                foreach (var kvp in packageToVersionDictionary)
                {
                    Console.WriteLine($"Package: {kvp.Key}");
                    Console.WriteLine($"Version: {kvp.Value}");
                }

                if (pass)
                {
                    bool hasDependencies = lines.Length > 1 + numPackages;
                    if (hasDependencies)
                    {
                        for (int j = 2+numPackages; j < lines.Length; j++)
                        {
                            if (!pass) break;
                            var subs = lines[j].Split(',');
                            int numDependencies = subs.Length / 2 - 1;
                            
                            if (packageToVersionDictionary.ContainsKey(subs[0]))
                            {
                                if (packageToVersionDictionary[subs[0]] == subs[1])
                                {
                                    for (int k = 0; k < 2 * numDependencies; k+=2)
                                    {
                                        if (packageToVersionDictionary.ContainsKey(subs[k + 2]) &&
                                            packageToVersionDictionary[subs[k + 2]] == subs[k + 3]) continue;
                                            pass = packageToVersionDictionary.TryAdd(subs[k+2], subs[k+3]);
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine(pass ? "PASS" : "FAIL");
            }
            
        }
    }
}