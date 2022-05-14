using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace TextAnalyzer
{
    class AnalysisResult
    {
        // File info
        [NonSerialized] public string filePath;
        [JsonPropertyName("fileSize")] public long FileSize { get; set; }
        [JsonPropertyName("filename")] public string FileName { get; set; }

        // Letters
        [JsonPropertyName("lettersCount")] public int LettersCount { get; set; }
        [JsonPropertyName("letters")] public SortedDictionary<string, int> Letters { get; set; }

        // Words
        [JsonPropertyName("words")] public SortedDictionary<string, int> Words { get; set; }
        [JsonPropertyName("wordsCount")] public int WordsCount { get; set; }
        [JsonPropertyName("uniqueWordsCount")] public int UniqueWordsCount { get; set; }
        [JsonPropertyName("longestWord")] public string LongestWord { get; set; }

        // Sentence
        [JsonPropertyName("longestSentence")] public string LongestSentence { get; set; }

        // Counts
        [JsonPropertyName("linesCount")] public int LinesCount { get; set; }
        [JsonPropertyName("digitsCount")] public int DigitsCount { get; set; }
        [JsonPropertyName("numbersCount")] public int NumbersCount { get; set; }
        [JsonPropertyName("longestSentenceWordsCount")] public int LongestSentenceWordsCount { get; set; }
        [JsonPropertyName("punctuation")] public int Punctuation { get; set; }

        public AnalysisResult()
        {
            FileSize = 0;
            LinesCount = 0;
            DigitsCount = 0;
            NumbersCount = 0;
            Punctuation = 0;
            LongestSentenceWordsCount = 0;

            LongestSentence = string.Empty;

            Letters = new SortedDictionary<string, int>();
            Words = new SortedDictionary<string, int>();
        }
    }
}
