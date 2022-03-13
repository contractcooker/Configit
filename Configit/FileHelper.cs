using System;
using System.Collections.Generic;
using System.IO;
using Ardalis.GuardClauses;

namespace Configit
{
    public class FileHelper
    {
        public string[] Lines { get; }
        public int NumPackages { get; }
        public int DependencyIndex { get; }

        public bool ValidConfig { get; }

        public FileHelper(string currentFile)
        {
            Guard.Against.NullOrEmpty(currentFile, nameof(currentFile));
            Lines = File.ReadAllLines(currentFile);
            NumPackages = GetNumPackages(Lines[0]);
            DependencyIndex = 2 + NumPackages;
            ValidConfig = Validate(Lines);
        }

        private bool Validate(string[] lines)
        {
            bool isValid;
            bool hasDependencies = lines.Length > 1 + NumPackages;
            Dictionary<string, string> packageList = new Dictionary<string, string>();
            for (int i = 1; i <= NumPackages; i++)
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
            
            for (int i = DependencyIndex; i < lines.Length; i++)
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
}