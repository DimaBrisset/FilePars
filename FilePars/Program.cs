namespace FilePars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Text text = new();
            text.ReadFile();
            text.CalcWords();
            text.PunctuationMarks();
            text.SearchChar();
            text.LongSentence();
            text.ShortOffer();
        }
    }
}