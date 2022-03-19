using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Hosting;

namespace Configit
{
    public class FileHelper : IFileHelper
    {
        //public string[] Lines { get; }
        
        //public int DependencyIndex { get; }

        //public bool ValidConfig { get; }
        //private readonly IIoHelper _iohelper;

        public FileHelper()
        {
            
            //DependencyIndex = 2 + NumPackages;
        }

        public bool Validate(string[] lines)
        {
            var numPackages = GetNumPackages(lines[0]);
            var dependencyIndex = 2 + numPackages;
            bool isValid;
            bool hasDependencies = lines.Length > 1 + numPackages;
            Dictionary<string, string> packageList = new Dictionary<string, string>();
            for (int i = 1; i <= numPackages; i++)
            {
                var subs = lines[i].Split(',');
                if (ContainsIdenticalPackage(packageList, subs)) continue;
                isValid = packageList.TryAdd(subs[0], subs[1]);
                if (!isValid)
                {
                    return false;
                }
            }

            if (!hasDependencies) return true;
            
            for (int i = dependencyIndex; i < lines.Length; i++)
            {
                var subs = lines[i].Split(',');
                int numDependencies = subs.Length / 2 - 1;
                if (ContainsIdenticalPackage(packageList, subs))
                {
                    for (int j = 2; j <= 2 * numDependencies; j+=2)
                    {
                        if (ContainsIdenticalPackage(packageList, subs[j], subs[j+1]))
                        {
                            continue;
                        }
                        isValid = packageList.TryAdd(subs[j], subs[j + 1]);
                        if (!isValid)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static bool ContainsIdenticalPackage(Dictionary<string, string> packageList, string[] subs)
        {
            return packageList.ContainsKey(subs[0]) && packageList[subs[0]] == subs[1];
        }

        private static bool ContainsIdenticalPackage(Dictionary<string, string> packageList, string key, string value)
        {
            return packageList.ContainsKey(key) && packageList[key] == value;
        }

        private static int GetNumPackages(string line)
        {
            bool success = int.TryParse(line, out var numPackages);
            return success ? numPackages : throw new FormatException("File is improperly formatted");
        }
    }

    public interface IFileHelper
    {
        bool Validate(string[] lines);
    }
}