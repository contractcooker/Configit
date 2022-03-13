using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Resolve_dependencies
{
    public class FileHelper
    {
        public string[] Lines { get; set; }
        public int NumPackages { get; set; }
        public List<KeyValuePair<string, string>> PackageList { get; set; }
        
        public FileHelper(string currentFile)
        {
            Lines = File.ReadAllLines(currentFile);
            NumPackages = GetNumPackages(Lines[0]);
            for (int i = 1; i <= NumPackages; i++)
            {
                var subs = Lines[i].Split(',');
                PackageList.Add(new KeyValuePair<string, string>(subs[0], subs[1]));
            }
        }

        public static int GetNumPackages(string line)
        {
            int numPackages;
            bool success = int.TryParse(line, out numPackages);
            return success ? numPackages : throw new FormatException("File is improperly formatted");
        }
    }
}