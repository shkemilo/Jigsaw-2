using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games.LetterOnLetter
{
    public class LoLGUI : IAnimatableGUI
    {
        private readonly Queue<IAnimatableGUI> animatableDisplays;
        private readonly List<IAnimatableGUI> animatableDisplaysCopy;

        public LoLGUI(IEnumerable<IAnimatableGUI> animatableDisplays)
        {
            this.animatableDisplays = new Queue<IAnimatableGUI>(animatableDisplays);

            animatableDisplaysCopy = new List<IAnimatableGUI>(animatableDisplays);
        }

        public void Next()
        {
            animatableDisplays.Peek().AnimationStop();
            animatableDisplays.Dequeue().Print();
        }

        public void AnimationStop()
        {
            foreach (IAnimatableGUI element in animatableDisplaysCopy)
            {
                element.AnimationStop();
            }
        }

        public void Print()
        {
            if (animatableDisplays.Count != 0)
            {
                animatableDisplays.Peek().Print();
            }
        }

        public void Animate()
        {
            if (animatableDisplays.Count != 0)
            {
                animatableDisplays.Peek().Animate();
            }
        }

        public void Enable(bool b)
        {
            foreach (IAnimatableGUI element in animatableDisplaysCopy)
            {
                element.Enable(b);
            }
        }

        public void Update<T>(T message)
        {
            if (message is char[])
            {
                updateElements(message as char[]);
            }
            else
            {
                throw new ArgumentException("This function only accepts char[]");
            }
        }

        private void updateElements(char[] letters)
        {
            int i = 0;

            foreach (IObserver observer in animatableDisplaysCopy)
            {
                observer.Update(letters[i++]);
            }
        }
    }
}