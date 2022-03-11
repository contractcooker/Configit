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
                Console.WriteLine(filename);
                
                var lines = File.ReadAllLines(filename);
                int numPackages = int.Parse(lines[0]);
                int numDependencies;
                if (lines.Length > 1 + numPackages)
                {
                    numDependencies = int.Parse(lines[1 + numPackages]);
                }
                else numDependencies = 0;
                
                Console.WriteLine($"Number of packages: {numPackages}");
                Console.WriteLine($"Number of dependencies: {numDependencies}");

                Dictionary<string, int> packageToVersionDictionary = new Dictionary<string, int>();
                for (int j = 1; j <= numPackages; j++)
                {
                    var subs = lines[j].Split(',');
                    pass = packageToVersionDictionary.TryAdd(subs[0],int.Parse(subs[1]));
                    
                    foreach (var kvp in packageToVersionDictionary)
                    {
                        Console.WriteLine($"Package: {kvp.Key}");
                        Console.WriteLine($"Version: {kvp.Value}");
                    }
                    // foreach (var s in subs)
                    // {
                    //     Console.WriteLine(s);
                    // }
                }

                Console.WriteLine(pass ? "PASS" : "FAIL");

                foreach (var line in lines)
                {
                   // Console.WriteLine(line);
                }
            }
            
        }
    }
}