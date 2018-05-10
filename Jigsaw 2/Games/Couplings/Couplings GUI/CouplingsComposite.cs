using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Jigsaw_2.Games.Couplings
{
    internal class CouplingsComposite : IGUI
    {
        public static readonly SolidColorBrush MatchesActiveColor = Brushes.Goldenrod;
        public static readonly SolidColorBrush MatchTargetsActiveColor = Brushes.Transparent;

        private readonly Queue<IGUI> matches;
        private readonly List<IGUI> matchTargets;

        private readonly int numberOfFields;

        public CouplingsComposite(IEnumerable<IGUI> matches, IEnumerable<IGUI> matchTargets, int numberOfFields = 8)
        {
            this.matches = new Queue<IGUI>(matches);
            this.matchTargets = new List<IGUI>(matchTargets);

            this.numberOfFields = numberOfFields;

            foreach (IGUI element in matchTargets)
            {
                element.Update(MatchTargetsActiveColor);
            }

            this.matches.Peek().Update(MatchesActiveColor);
        }

        public void EvaluateAll()
        {
            foreach (IGUI element in matchTargets)
            {
                element.Update(Brushes.DarkRed);
            }
        }

        public void Next()
        {
            matches.Dequeue().Enable(false);

            if (matches.Count != 0)
            {
                matches.Peek().Update(MatchesActiveColor);
            }
        }

        public void Enable(bool b)
        {
            matches.Peek().Enable(b);
        }

        public void Print()
        {
            foreach (IGUI element in matches)
            {
                element.Print();
            }

            foreach (IGUI element in matchTargets)
            {
                element.Print();
            }
        }

        public void Update<T>(T message)
        {
            if (message is SolidColorBrush)
            {
                updateMatches(message as SolidColorBrush);
            }
            else if (message is Tuple<int, SolidColorBrush>)
            {
                updateMatchTargets(message as Tuple<int, SolidColorBrush>);
            }
            else
            {
                throw new Exception("Invalid function call.");
            }
        }

        /// <summary> Updates the active match by converting a object to a boolean. </summary>
        private void updateMatches(SolidColorBrush message)
        {
            matches.Peek().Update(message);
        }

        /// <summary> Updates the match target by interpreting the message as a Tuple. </summary>
        private void updateMatchTargets(Tuple<int, SolidColorBrush> message)
        {
            matchTargets.ElementAt(message.Item1).Update(message.Item2);
        }
    }
}