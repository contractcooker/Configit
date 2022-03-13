namespace Configit
{
    class Program
    {
        static void Main(string[] args)
        {
            IoHelper ioh = new IoHelper();
            var inputFiles = ioh.GetInputFiles();
            foreach (string currentFile in inputFiles)
            {
                FileHelper fh = new FileHelper(currentFile);
                ioh.CreateOutputFile(currentFile, fh.ValidConfig ? "PASS" : "FAIL");
            }
        }
    }
}