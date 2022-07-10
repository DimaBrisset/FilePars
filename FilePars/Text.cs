using System.Text;
using System.Text.RegularExpressions;

namespace FilePars
{
    internal class Text
    {
        readonly string pathFile = @"E:\1\sample.txt";
        readonly string pathFileCalcWords = @"E:\1\CalcWords.txt";
        readonly string pathFileInformation = @"E:\1\Information.txt";
        readonly List<string> _wordsList = new();

        public void ReadFile()
        {
            using FileStream fs = new(pathFile, FileMode.Open, FileAccess.Read);
            using StreamReader sr = new(fs, Encoding.Default);

            string[] separators = new string[] { " ", "\r\n", "\t", "\"" };
            char[] delimiterChar = { '.', '!', '?', '"', '\'', '\'' };
            while (sr.ReadLine() is { } line)
            {
                line = Regex.Replace(line, @"\s+", " ", RegexOptions.Multiline);
                line = Regex.Replace(line, @"[\d-]", string.Empty);
                line = Regex.Replace(line, "[!\"#$%&'()*+,-./:;<=>?@\\[\\]^_`{|}~]", string.Empty);
                _ = line.Trim(delimiterChar);
                _wordsList.AddRange(line.ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries));
            }

            _wordsList.Sort();
        }

        public void CalcWords()
        {
            var dictonory = new Dictionary<string, int>();

            foreach (var lines in _wordsList)
            {
                if (dictonory.ContainsKey(lines))
                {
                    dictonory[lines]++;
                }
                else
                {
                    dictonory.Add(lines, 1);
                }
            }

            using StreamWriter sw = new(pathFileCalcWords);

            foreach (var item in dictonory)
            {
                sw.WriteLine(item.ToString());
            }
        }

        public void PunctuationMarks()
        {
            string text = File.ReadAllText(pathFile);
            var dot = Regex.Matches(text, @"[\.]").Count;
            var questionMark = Regex.Matches(text, @"[\?]").Count;
            var exclamationPoint = Regex.Matches(text, @"[\!]").Count;
            var comma = Regex.Matches(text, @"[\,]").Count;
            var count = Regex.Matches(text, @"[\.,!\?]").Count;

            using StreamWriter sw = File.AppendText(pathFileInformation);
            sw.WriteLine($"(.) used ={dot}");
            sw.WriteLine($"(?) used={questionMark}");
            sw.WriteLine($"(!) used={exclamationPoint}");
            sw.WriteLine($"(,) used={comma}");
            sw.WriteLine($"Total Punctuation Marks ={count}");
            sw.WriteLine(Environment.NewLine);
        }

        public void SearchChar()
        {
            int count = 0;
            char letter = ' ';
            string text = File.ReadAllText(pathFile).ToLower();

            for (char symbol = 'a'; symbol < 'z'; symbol++)
            {
                int temp = 0;
                for (int i = 0; i < text.Length; i++)
                    if (text[i] == symbol)
                        temp++;

                if (temp > count)
                {
                    count = temp;
                    letter = symbol;
                }
            }

            using StreamWriter sw = File.AppendText(pathFileInformation);
            sw.WriteLine($"Letter ({letter}) used {count} times");
            sw.WriteLine(Environment.NewLine);
        }

        public void LongSentence()
        {
            string input = File.ReadAllText(pathFile);
            using StreamWriter sw = File.AppendText(pathFileInformation);
            sw.WriteLine("LongSentence:\n" +
                input.Split(new[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                    .OrderByDescending(s => s.Length).First());
            sw.WriteLine(Environment.NewLine);
        }

        public void ShortOffer()
        {
            string input = File.ReadAllText(pathFile);
            using StreamWriter sw = File.AppendText(pathFileInformation);
            sw.WriteLine(("ShortOffer:\n" + input
                .Split(new[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries).OrderBy(s => s.Length)
                .First()));
            sw.WriteLine(Environment.NewLine);
        }
    }
}