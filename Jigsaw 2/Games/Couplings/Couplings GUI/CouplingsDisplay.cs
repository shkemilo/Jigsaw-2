using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;

namespace Jigsaw_2.Games.Couplings
{
    /// <summary>
    /// Represents the Couplings GUI for the Couplings Game.
    /// </summary>
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

        /// <summary> Updates the active match by converting a object to a boolean. </summary>
        private void updateMatches(object message)
        {
            matches.Peek().Update(Convert.ToBoolean(message));
        }

        /// <summary> Updates the match target by interperting the message as a Tuple. </summary>
        private void updateMatchTargets(object message)
        {
            Tuple<int, bool> temp = message as Tuple<int, bool>;

            matchTargets.ElementAt(temp.Item1).Update(temp.Item2);
        }

        /// <summary> Returns the current match. </summary>
        public CouplingsDisplayBase CurrentMatch()
        {
            return matches.Peek();
        }

        /// <summary> Returns all the match targets. </summary>
        public List<CouplingsDisplayBase> GetMatchTargets()
        {
            return matchTargets;
        }

        /// <summary> Removes and disables the current match then sets the next one active. </summary>
        public void NextMatch()
        {
            matches.Dequeue().GetMatch().IsEnabled = false; ;

            matches.Peek().SetActive(Brushes.Goldenrod);
        }

        /// <summary> Sets all the colors that haven't been evalueted to the color which represent a wrong answer. </summary>
        public void SetAllColors()
        {
            foreach (CouplingsDisplayBase element in matches)
                if (!element.IsEvaluated())
                    element.SetColor(false);

            foreach (CouplingsDisplayBase element in matchTargets)
                if (!element.IsEvaluated())
                    element.SetColor(false);
        }

        /// <summary> Shows all the fields. </summary>
        public override void Show()
        {
            foreach (CouplingsDisplayBase element in matches)
                element.Show();

            foreach (CouplingsDisplayBase element in matchTargets)
                element.Show();
        }

        /// <summary> Updates the matches or match targets based on the message. </summary>
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
