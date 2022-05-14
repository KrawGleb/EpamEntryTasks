using System;


namespace TextAnalyzer
{
    static class Program
    {
        static void Main(string[] args)
        {
            string targetFile;
            string destFile = "result.json";

            switch (args.Length)
            {
                case 0:
                    string helpMessage = "Arguments: [target file] [destination file]\n" +
                        "target file - file for analysis\n" +
                        "destination file - file to save the result (default result.json)";
                    Console.WriteLine(helpMessage);
                    return;
                case 1:
                    targetFile = args[0];
                    break;
                case 2:
                    targetFile = args[0];
                    destFile = args[1];
                    break;
                default:
                    Console.WriteLine("Invalid arguments");
                    return;
            }

            try
            {
                Analyzer analyzer = new Analyzer();
                analyzer.Analyze(targetFile, destFile);
            }
            catch
            {
                Console.WriteLine("Analysis failed");
            }
        }
    }
}
