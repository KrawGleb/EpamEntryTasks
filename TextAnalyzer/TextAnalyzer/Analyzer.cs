using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace TextAnalyzer
{
    class Analyzer
    {
        readonly char[] punctuationSymbols = { '.', ':', ',', ';' };
        readonly char[] sentenceEndingSymbols = { '.', '?', '!' };
        readonly string[] serviceSymbols = { "-", "\"", "\'", ")", "(", "[", "]", "?", "!", "\r", "\n", "..." };
        string prevSentencePart = string.Empty;

        readonly AnalysisResult analysisResult;
        
        public Analyzer()
        {
            analysisResult = new AnalysisResult();
        }

        public void Analyze(string targetFile, string destFile)
        {
            analysisResult.filePath = targetFile;

            try
            {
                FileInfo fileInfo = new FileInfo(targetFile);
                analysisResult.FileSize = fileInfo.Length;
                DateTime startTime = DateTime.Now;

                AnalyzeFile();
                
                SetFileName();
                SetLettersCount();
                SetWordsCount();
                SetWordsCount();
                SetUniqueWordsCount();
                SetLongestWord();

                SaveResult(destFile);

                Console.WriteLine($"Analysis Completed in {DateTime.Now - startTime}");

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch
            {
                Console.WriteLine("Unexpected exception.\nAnalysis failed");
            }
        }

        public void SetFileName() => analysisResult.FileName = Path.GetFileName(analysisResult.filePath);
        public void SetLettersCount() => analysisResult.LettersCount = analysisResult.Letters.Values.Sum();
        public void SetWordsCount() => analysisResult.WordsCount = analysisResult.Words.Values.Sum();
        public void SetUniqueWordsCount() =>
            analysisResult.UniqueWordsCount = analysisResult.Words.Keys.Count;
        public void SetLongestWord()
        {
            if (analysisResult.Words.Keys.Count == 0)
            {
                analysisResult.LongestWord = string.Empty;
            }
            int maxLength = analysisResult.Words.Keys.Select(word => word.Length).Max();
            analysisResult.LongestWord = analysisResult.Words.Keys.FirstOrDefault(word => word.Length == maxLength);
        }

        public void AnalyzeFile()
        {
            try
            {
                FileReader fileReader = new FileReader(analysisResult.filePath);
                foreach (string textBlock in fileReader)
                {
                    AnalyzeTextBlock(textBlock);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
        }

        public void AnalyzeTextBlock(string textBlock)
        {
            try
            {
                FindLongestSentence(textBlock);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("An empty string received");
            }

            string[] lines = textBlock.Split('\n');
            analysisResult.LinesCount += lines.Length - 1;

            foreach (var line in lines)
            {
                AnalyzeLine(line);
            }
        }

        public void FindLongestSentence(string textBlock)
        {
            if (string.IsNullOrEmpty(textBlock))
            {
                throw new ArgumentNullException(nameof(textBlock));
            }

            textBlock = textBlock.Replace("\n", "");
            string[] sentences = textBlock.Split(sentenceEndingSymbols, StringSplitOptions.RemoveEmptyEntries);

            // Trim all sentences
            sentences = sentences.Select(sentence => sentence.Trim()).ToArray();

            if (sentences.Length > 0)
            {
                if (!string.IsNullOrEmpty(prevSentencePart))
                {
                    sentences[0] = sentences[0][0] == ' ' || prevSentencePart[^1] == ' ' ? 
                        string.Concat(prevSentencePart, sentences[0]) : 
                        string.Concat(prevSentencePart, " ", sentences[0]);
                }

                if (!".?!".Contains(textBlock[^1]))
                {
                    prevSentencePart = sentences[^1];
                }

                string longestSentence = sentences.OrderBy((sentece) => sentece.Length).LastOrDefault();

                if (longestSentence.Length > analysisResult.LongestSentence.Length)
                {
                    analysisResult.LongestSentence = longestSentence;
                    analysisResult.LongestSentenceWordsCount = longestSentence.Split(new char[] { ' ', '\'', '-' }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }
        }

        public void AnalyzeLine(string line)
        {
            string[] words = line.Split(new char[] { ' ', '\'', '\"' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0)
            {
                AnalyzeWords(words);
            }
        }

        public void AnalyzeWords(string[] words)
        {
            for (int wordIndex = 0; wordIndex < words.Length; wordIndex++)
            {
                words[wordIndex] = words[wordIndex].Trim(serviceSymbols.Select(ch => ch[0]).ToArray());
                string trimmedWord = words[wordIndex].Trim(punctuationSymbols);

                analysisResult.Punctuation += words[wordIndex].Length - trimmedWord.Length;

                AnalyzeWord(trimmedWord.Trim(' '));
            }
        }

        public void AnalyzeWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return;
            }

            word = word.ToLower();

            foreach (var serviceSymbol in serviceSymbols)
            {
                word = word.Replace(serviceSymbol, "");
            }

            string[] splittedByPunctuation = word.Split(punctuationSymbols, StringSplitOptions.RemoveEmptyEntries);
            if (splittedByPunctuation.Length > 1)
            {
                analysisResult.Punctuation += splittedByPunctuation.Length - 1;
                foreach (var wordPart in splittedByPunctuation)
                {
                    AnalyzeWord(wordPart);
                }
                return;
            }

            // Numbers count
            if (word.All((ch) => char.IsDigit(ch)))
            {
                analysisResult.DigitsCount += word.Length;
                analysisResult.NumbersCount += 1;
                return;
            }

            // Words
            if (analysisResult.Words.Keys.Contains(word))
            {
                analysisResult.Words[word]++;
            }
            else
            {
                analysisResult.Words.Add(word, 1);
            }

            // Letters
            Dictionary<string, int> letters = word.Where(ch => char.IsLetter(ch)).GroupBy(ch => ch).ToDictionary(k => k.Key.ToString(), v => v.Count());

            foreach (var letter in letters)
            {
                if (analysisResult.Letters.Keys.Contains(letter.Key))
                {
                    analysisResult.Letters[letter.Key] += letter.Value;
                }
                else
                {
                    analysisResult.Letters.Add(letter.Key, letter.Value);
                }
            }
        }

        public void SaveResult(string destFile)
        {
            string json = string.Empty;
            try
            {
                json = JsonSerializer.Serialize<AnalysisResult>(analysisResult);
            }
            catch
            {
                Console.WriteLine("Conversion to json error");
            }

            try
            {
                FileMode fileMode = File.Exists(destFile) ? FileMode.Truncate : FileMode.Create;
                using StreamWriter streamWriter = new StreamWriter(new FileStream(destFile, fileMode));

                streamWriter.Write(json);
            }
            catch
            {
                Console.WriteLine("Save error");
            }
        }
    }
}
