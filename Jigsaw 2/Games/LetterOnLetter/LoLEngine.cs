using System;
using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;

namespace Jigsaw_2.Games.LetterOnLetter
{
    /// <summary>
    /// Engine for the Letter on Letter Game.
    /// </summary>
    public class LoLEngine : Engine
    {
        int numberOfFields;
        string word;
        char[] letters;

        public LoLEngine(int numberOfFields)
        {
            this.numberOfFields = numberOfFields;

            letters = new char[numberOfFields];

            word = WordList.Instance.GetWoWSeed();

            #if DEBUG
                Console.WriteLine(word);
            #endif

            generateLetters();
        }

        /// <summary> Generates the letters that the user will combine. </summary>
        private void generateLetters()
        {
            /*string word = "";
            for (int i = 0; i < words.Length; i++)
                word += words[i];*/

            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = word.Length; i < numberOfFields; i++)
                word += (char)('A' + rnd.Next(0, 26));

            char[] wordArray = word.ToCharArray();

            new Random().Shuffle(wordArray);

            letters = wordArray;
        }

        /// <summary> Returns the longest word that can be made with the current letters. </summary>
        public string GetLongestWord()
        {
            return word;
        }

        /// <summary> Returns the letters that the user will combine. </summary>
        public char[] GetLetters()
        {
            return letters;
        }

        /// <summary> Check if the specified word is a viable word. </summary>
        public bool Check(string s)
        {
            return WordList.Instance.Check(s);
        }
    }
}
