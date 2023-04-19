using System;
using System.Collections.Generic;

namespace pendu
{
    public class WordGenerator
    {
        private static readonly List<Word> words = new List<Word>()
        {
            new Word("Programmation"),
            new Word("Pentiminax"),
            new Word("Soleil"),
            new Word("Immeuble"),
            new Word("Canapé")
        };

        private static readonly Random random = new Random();

        public static Word GetRandomWord()
        {
            return words[random.Next(words.Count)];
        }
    }
}
