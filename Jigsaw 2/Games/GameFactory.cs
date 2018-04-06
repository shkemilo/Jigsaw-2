﻿using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games
{
    /// <summary>
    /// Static class that represents a factory for creating Games and their associated pages
    /// </summary>
    public static class GameFactory
    {
        #region Private Static Fields

        private static Dictionary<string, string> instructions;

        #endregion Private Static Fields

        #region Constructors

        static GameFactory()
        {
            instructions = new Dictionary<string, string>
            {
                { "letteronletter", "In this game the goal is to find the longest word out of the given letters. \nBy clicking on the letter you can add them to your answer. \nBy clicking on the Undo sign you can undo your last selected letter. \nBy clicking on the book with a question mark you can check whether your word is in our Dictionary. \nWhen you are finished you can click the Submit button. \nGood Luck!" },
                { "jumper", "In this game the goal is to find the correct combination of symbols. \nBy pressing on the symbols you add them to your answer. \nYou can undo your last sign by clicking on it. \nWhen you are finished with your click the top-most button to check your answer and move on to the next row. \nGood Luck!" },
                { "couplings", "In this game the goal is to match the words on screen by following the displayed rule. \nGood Luck!" }
            };
        }

        #endregion Constructors

        #region Public Static Methods

        /// <summary> Creates and returns a GamePage based on the games name. </summary>
        public static GamePage GetGame(string s)
        {
            s = s.ToLower();
            if (s == "letteronletter")
                return new LetterOnLetter.LetterOnLetter();
            else if (s == "jumper")
                return new Jumper.Jumper();
            else if (s == "couplings")
                return new Couplings.Couplings();

            throw new Exception("Game does not exist. (yet)");
        }

        public static string GetInstructions(string s)
        {
            s = s.ToLower();

            if (instructions.ContainsKey(s))
                return instructions[s];
            else
                throw new Exception("Game does not exist. (yet");
        }

        #endregion Public Static Methods
    }
}