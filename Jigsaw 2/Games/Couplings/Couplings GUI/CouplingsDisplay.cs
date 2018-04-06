using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Jigsaw_2.Games.Couplings
{
    /// <summary>
    /// Represents the Couplings GUI for the Couplings Game.
    /// </summary>
    internal class CouplingsDisplay : GUIElement
    {
        #region Private Static ReadOnly Fields

        private static readonly SolidColorBrush matchesActiveColor = Brushes.Goldenrod;
        private static readonly SolidColorBrush matchTargetsActiveColor = Brushes.Transparent;

        #endregion Private Static ReadOnly Fields

        #region Private Fields

        private Queue<CouplingsDisplayBase> matches;
        private List<CouplingsDisplayBase> matchTargets;

        private int numberOfFields;

        #endregion Private Fields

        #region Constructors

        public CouplingsDisplay(Queue<CouplingsDisplayBase> matches, List<CouplingsDisplayBase> matchTargets, int numberOfFields = 8)
        {
            this.matches = matches;
            this.matchTargets = matchTargets;

            this.numberOfFields = numberOfFields;

            foreach (CouplingsDisplayBase element in matchTargets)
                element.SetActive(matchTargetsActiveColor);

            matches.Peek().SetActive(matchesActiveColor);
        }

        #endregion Constructors

        #region Public Methods

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
            matches.Dequeue().GetMatch().IsEnabled = false;

            matches.Peek().SetActive(matchesActiveColor);
        }

        /// <summary> Sets all the colors that haven't been evaluated to the color which represent a wrong answer. </summary>
        public void SetAllColors()
        {
            setAllWrong(matches);

            setAllWrong(matchTargets);
        }

        #endregion Public Methods

        #region Public Override Methods

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

        /// <summary> Shows all the fields. </summary>
        public override void Show()
        {
            foreach (CouplingsDisplayBase element in matches)
                element.Show();

            foreach (CouplingsDisplayBase element in matchTargets)
                element.Show();
        }

        #endregion Public Override Methods

        #region Private Methods

        private void setAllWrong(IEnumerable<CouplingsDisplayBase> elements)
        {
            foreach (CouplingsDisplayBase element in elements)
                if (!element.IsEvaluated())
                    element.SetColor(false);
        }

        /// <summary> Updates the active match by converting a object to a boolean. </summary>
        private void updateMatches(object message)
        {
            matches.Peek().Update(Convert.ToBoolean(message));
        }

        /// <summary> Updates the match target by interpreting the message as a Tuple. </summary>
        private void updateMatchTargets(object message)
        {
            Tuple<int, bool> temp = message as Tuple<int, bool>;

            matchTargets.ElementAt(temp.Item1).Update(temp.Item2);
        }

        #endregion Private Methods
    }
}