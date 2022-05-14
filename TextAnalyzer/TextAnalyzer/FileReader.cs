using System;
using System.IO;
using System.Collections;


namespace TextAnalyzer
{
    class FileReader
    {
        const int blockSize = 100;
        readonly string path;

        public FileReader(string textPath)
        {
            path = textPath;
        }

        public IEnumerator GetEnumerator()
        {
            using StreamReader targetFile = new StreamReader(path);
            string output = string.Empty;
            int linesCount = 0;
            string nextLine;

            while ((nextLine = targetFile.ReadLine()) != null)
            {
                output = string.Concat(output, nextLine, "\n");
                linesCount++;

                if (linesCount == blockSize)
                {
                    linesCount = 0;
                    yield return output;

                    output = string.Empty;
                }
            }

            if (output.Length != 0)
            {
                yield return output;
            }
        }
    }
}
