using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games.LetterOnLetter
{
    /// <summary>
    /// Singleton that is used for storing the Word List used for the Word on Word game.
    /// </summary>
    public sealed class WordList // TODO: Save whole object and just load when game starts, to prevent high cpu usage.
    {
        #region Private Fields

        private readonly Random rnd;

        private readonly string[] wordList;
        private readonly List<string> WoWSeeds;

        #endregion Private Fields

        #region Constructors

        public WordList()
        {
            wordList = Properties.Resources.WordList.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            WoWSeeds = new List<string>();

            getWoWSeeds();

            rnd = new Random(Guid.NewGuid().GetHashCode());
        }

        #endregion Constructors

        #region Public Methods

        /// <summary> Returns a random 12 letter word. </summary>
        public string GetWoWSeed()
        {
            return WoWSeeds[rnd.Next(0, WoWSeeds.Count - 1)];
        }

        /// <summary> Check if the word is inside the Word list. </summary>
        public bool Check(string s)
        {
            if (s != string.Empty)
            {
                for (int i = 0; i < wordList.Length; i++)
                {
                    if (s.Equals(wordList[i], StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary> Finds all words with 12 length. </summary>
        private void getWoWSeeds()
        {
            for (int i = 0; i < wordList.Length; i++)
            {
                if (wordList[i].Length == 12)
                {
                    WoWSeeds.Add(wordList[i].ToUpper());
                }
            }
        }

        #endregion Private Methods
    }
}