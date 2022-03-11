using System;
using System.IO;

namespace Resolve_dependencies
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string filepath = @"/Users/husker/Downloads/testdata/";
            string filename;
            for (int i = 0; i < 10; i++)
            {
                filename = filepath + "input00" + i + ".txt";
                Console.WriteLine(filename);
                var lines = File.ReadAllLines(filename);
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            
        }
    }
}