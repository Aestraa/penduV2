namespace pendu
{
    public class Word
    {
        public string Text { get; }
        public int Length => Text.Length;

        public Word(string text)
        {
            Text = text.ToUpper();
        }

        public int GetIndexOf(char letter)
        {
            return Text.IndexOf(letter);
        }

        public int GetIndexOf(char letter, int startIndex)
        {
            return Text.IndexOf(letter, startIndex);
        }

    }
}
