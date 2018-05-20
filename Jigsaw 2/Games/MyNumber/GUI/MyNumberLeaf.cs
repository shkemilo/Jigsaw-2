using Jigsaw_2.Abstracts;
using System;
using System.Windows.Controls;

namespace Jigsaw_2.Games.MyNumber
{
    internal class MyNumberLeaf : IAnimatableGUI
    {
        private Button field;
        private string realValue;

        private string toPrint;

        private readonly Random animationSeed;

        private readonly int[] viableNumbers;

        public MyNumberLeaf(Button field, int[] viableNumbers)
        {
            this.field = field;

            realValue = string.Empty;
            toPrint = string.Empty;

            animationSeed = new Random();

            this.viableNumbers = viableNumbers;
        }

        public void Animate()
        {
            int index = animationSeed.Next(viableNumbers.Length);

            toPrint = viableNumbers[index].ToString();
        }

        public void AnimationStop()
        {
            toPrint = realValue;
        }

        public void Enable(bool b)
        {
            field.IsEnabled = b;
        }

        public void Print()
        {
            field.Content = toPrint;
        }

        public void Update<T>(T message)
        {
            if (message is string)
            {
                realValue = message as string;
            }
            else
            {
                throw new ArgumentException("Invalid argument");
            }
        }
    }
}