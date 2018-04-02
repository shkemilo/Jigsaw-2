using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;

namespace Jigsaw_2.Games.Couplings
{
    class CouplingsDisplay : GUIElement
    {
        Queue<CouplingsDisplayBase> matches;
        List<CouplingsDisplayBase> matchTargets;

        int numberOfFields;

        public CouplingsDisplay(Queue<CouplingsDisplayBase> matches, List<CouplingsDisplayBase> matchTargets, int numberOfFields = 8)
        {
            this.matches = matches;
            this.matchTargets = matchTargets;

            this.numberOfFields = numberOfFields;

            foreach (CouplingsDisplayBase element in matchTargets)
                element.SetActive(Brushes.White);

            matches.Peek().SetActive(Brushes.Goldenrod);
        }

        public CouplingsDisplayBase CurrentMatch()
        {
            return matches.Peek();
        }

        public void SetAllColors()
        {
            foreach (CouplingsDisplayBase element in matches)
                if (!element.IsEvaluated())
                    element.SetColor(false);

            foreach (CouplingsDisplayBase element in matchTargets)
                if (!element.IsEvaluated())
                    element.SetColor(false);
        }

        public void NextMatch()
        {
            matches.Dequeue().GetMatch();

            matches.Peek().SetActive(Brushes.Goldenrod);
        }

        public List<CouplingsDisplayBase> GetMatchTargets()
        {
            return matchTargets;
        }

        public override void Show()
        {
            foreach (CouplingsDisplayBase element in matches)
                element.Show();

            foreach (CouplingsDisplayBase element in matchTargets)
                element.Show();
        }

        private void updateMatches(object message)
        {
            matches.Peek().Update(Convert.ToBoolean(message));
        }

        private void updateMatchTargets(object message)
        {
            Tuple<int, bool> temp = message as Tuple<int, bool>;

            matchTargets.ElementAt(temp.Item1).Update(temp.Item2);
        }

        public override void Update<T>(T message)
        {
            if (message is bool)
                updateMatches(message);
            else if (message is Tuple<int, bool>)
                updateMatchTargets(message);
            else
                throw new Exception("Invalid function call.");
        }
    }
}
