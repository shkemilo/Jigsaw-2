using Jigsaw_2.Abstracts;
using System;
using System.Windows.Controls;

namespace Jigsaw_2.Games.LetterOnLetter
{
    public class AnimatableDisplay : IAnimatableGUI
    {
        private readonly Button display;
        private char realValue;

        private char toDisplay;

        private readonly Random animationSeed;

        public AnimatableDisplay(Button display)
        {
            this.display = display;

            realValue = ' ';
            toDisplay = 'A';

            animationSeed = new Random(Guid.NewGuid().GetHashCode());
        }

        public void Animate()
        {
            toDisplay = (char)('A' + animationSeed.Next(0, 26));
        }

        public void AnimationStop()
        {
            toDisplay = realValue;
        }

        public void Print()
        {
            display.Content = toDisplay.ToString();
        }

        public void Enable(bool b)
        {
            display.IsEnabled = b;
        }

        public void Update<T>(T message)
        {
            if (message is char)
            {
                realValue = Convert.ToChar(message);
                Console.WriteLine(realValue);
            }
            else
            {
                throw new ArgumentException("This function only accepts char");
            }
        }
    }
}