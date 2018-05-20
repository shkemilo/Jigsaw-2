using Jigsaw_2.Abstracts;
using Jigsaw_2.Games.LetterOnLetter;
using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games.MyNumber
{
    internal class MyNumberComposite : LoLGUI
    {
        public MyNumberComposite(IEnumerable<IAnimatableGUI> animatableDisplays) : base(animatableDisplays)
        {
        }

        public override void Update<T>(T message)
        {
            if (message is List<string>)
            {
                updateElements(message as List<string>);
            }
            else
            {
                throw new ArgumentException("This function only accepts List<string>");
            }
        }

        private void updateElements(List<string> numbers)
        {
            int i = 0;

            foreach (IObserver observer in animatableDisplaysCopy)
            {
                observer.Update(numbers[i++]);
            }
        }
    }
}