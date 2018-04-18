using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Jigsaw_2.Games.LetterOnLetter
{
    /// <summary>
    /// Display for the user input GUI of Letter on Letter Game.
    /// </summary>
    public class LoLDisplay : GUIElement, IAnimateable
    {
        #region Private Fields

        private Control[] fields;

        private int numberOfFields;
        private int shownLetters;
        private char[] lettersToShow;
        private char[] letters;
        private Random rnd;

        #endregion Private Fields

        #region Constructors

        public LoLDisplay(Control[] fields, int numberOfFields)
        {
            this.fields = fields;
            this.numberOfFields = numberOfFields;

            lettersToShow = new char[numberOfFields];
            shownLetters = 0;

            rnd = new Random(Guid.NewGuid().GetHashCode());
        }

        #endregion Constructors

        #region Public Methods

        public List<Control> GetFields()
        {
            return fields.ToList();
        }

        /// <summary> Animates a field of the main display. </summary>
        public void Animate()
        {
            if (shownLetters < numberOfFields)
                lettersToShow[shownLetters] = (char)('A' + rnd.Next(0, 26));
        }

        /// <summary> Stops animating the current letter, and a </summary>
        public void UncoverLetter()
        {
            if ((shownLetters < numberOfFields) && (letters != null))
            {
                lettersToShow[shownLetters] = letters[shownLetters];
                Show();

                shownLetters++;
            }
        }

        #endregion Public Methods

        /// <summary> Return true if the display has shown all letters. </summary>
        public bool Finished()
        {
            if (shownLetters == numberOfFields)
                return true;
            else
                return false;
        }

        #region Public Override Methods

        /// <summary> Updates the display of the letters to be shown. </summary>
        public override void Update<T>(T message)
        {
            if (message is char[])
                letters = message as char[];
            else
                throw new Exception("This function only accepts char[]");
        }

        /// <summary> Show the letter on the current field. </summary>
        public override void Show()
        {
            if (shownLetters < numberOfFields)
                (fields[shownLetters] as Button).Content = lettersToShow[shownLetters].ToString();
        }

        #endregion Public Override Methods
    }
}