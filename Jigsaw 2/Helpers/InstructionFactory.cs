using Jigsaw_2.Abstracts;
using Jigsaw_2.Games;
using System;
using System.Collections.Generic;

namespace Jigsaw_2.Helpers
{
    internal class InstructionFactory : AbstractFactory
    {
        private Dictionary<string, string> instructions;

        public InstructionFactory()
        {
            instructions = new Dictionary<string, string>
            {
                { "letteronletter", "In this game the goal is to find the longest word out of the given letters. \nBy clicking on the letter you can add them to your answer. \nBy clicking on the Undo sign you can undo your last selected letter. \nBy clicking on the book with a question mark you can check whether your word is in our Dictionary. \nWhen you are finished you can click the Submit button. \nGood Luck!" },
                { "jumper", "In this game the goal is to find the correct combination of symbols. \nBy pressing on the symbols you add them to your answer. \nYou can undo your last sign by clicking on it. \nWhen you are finished with your click the top-most button to check your answer and move on to the next row. \nGood Luck!" },
                { "couplings", "In this game the goal is to match the words on screen by following the displayed rule. \nGood Luck!" }
            };
        }

        public override string GetInstructions(string game)
        {
            game = game.ToLower();

            if (instructions.ContainsKey(game))
                return instructions[game];
            else
                return null;
        }

        public override GamePage GetGame(string game)
        {
            return null;
        }
    }
}