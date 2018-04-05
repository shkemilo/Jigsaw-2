using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Jigsaw_2.Score;
using Jigsaw_2.Animators;

namespace Jigsaw_2.Games.Couplings
{
    /// <summary>
    /// Represents the Couplings Game.
    /// </summary>
    class CouplingsGame : Game
    {
        CouplingsEngine engine;
        CouplingsDisplay display;

        int numberOfFields;
        int currentFieldIndex;

        int valueOfField;

        public CouplingsGame(CouplingsEngine engine, Grid gameGrid, int numberOfFields = 8) : base(gameGrid, "couplings")
        {
            this.engine = engine;

            this.numberOfFields = numberOfFields;
            currentFieldIndex = 0;

            valueOfField = 2;

            (Finder.FindElementWithTag(allControls, "StartButton") as Button).Click += start;
        }

        /// <summary> Gets all the matches from the GUI. </summary>
        private Queue<CouplingsDisplayBase> getMatches(string[] content)
        {
            Queue<CouplingsDisplayBase> tempMatches = new Queue<CouplingsDisplayBase>();

            int z = 0;
            foreach (Control c in Finder.FindElementsWithTag(allControls, "Match"))
                tempMatches.Enqueue(new CouplingsDisplayBase(c as Button, content[z++]));

            return tempMatches;
        }

        /// <summary> Gets all the match targets from the GUI. </summary>
        private List<CouplingsDisplayBase> getMatchTargets(string[] content)
        {
            List<CouplingsDisplayBase> tempMatchTargets = new List<CouplingsDisplayBase>();

            int z = 0;
            foreach (Control c in Finder.FindElementsWithTag(allControls, "MatchTarget"))
                tempMatchTargets.Add(new CouplingsDisplayBase(c as Button, content[z++]));

            return tempMatchTargets;
        }

        /// <summary> Gets the matches content from the engine. </summary>
        private string[] getMatchesContent(Tuple<string, string>[] couplings)
        {
            string[] matchesContent = new string[numberOfFields];

            Console.WriteLine(couplings.Length);

            for (int i = 0; i < couplings.Length; i++)
                matchesContent[i] = couplings[i].Item1;

            return matchesContent;
        }

        /// <summary> Gets the match targets content from the engine. </summary>
        private string[] getMatchTargetsContent(Tuple<string, string>[] couplings)
        {
            string[] matchTargetsContent = new string[numberOfFields];

            for (int i = 0; i < couplings.Length; i++)
                matchTargetsContent[i] = couplings[i].Item2;

            return matchTargetsContent;
        }

        /// <summary> Sets the display. </summary>
        private void setDisplay(Tuple<string, string>[] couplings)
        {
            display = new CouplingsDisplay(getMatches(getMatchesContent(couplings)), getMatchTargets(getMatchTargetsContent(couplings)));
        }

        /// <summary> Changes to a new couplings. </summary>
        private void nextCoupling()
        {
            if (currentFieldIndex < numberOfFields - 1)
            {
                display.NextMatch();
                currentFieldIndex++;
            }
            else
                GameOver();
        }

        /// <summary> Updates the couplings based on their correctness. </summary>
        private void update(bool correct, int buttonIndex)
        {
            display.Update(correct);

            if (correct)
            {
                Grader();

                display.Update(new Tuple<int, bool>(buttonIndex, correct));
            }

            display.Show();
        }

        /// <summary> checks if  </summary>
        private bool check(string match, string matchTarget)
        {
            return engine.Check(match, matchTarget);
        }

        /// <summary> Gets the index of the button by finding its reference in the match targets. </summary>
        private int getIndexOfButton(Button b)
        {
            int i = 0;
            foreach (CouplingsDisplayBase element in display.GetMatchTargets())
                if (b == element.GetMatch())
                    return i;
                else
                    i++;

            throw new Exception("Button does not exsist.");
        }

        /// <summary> Animates and orders the couplings in the correct order when the game ends. </summary>
        private void orderCouplings()
        {
            int[] offset = engine.GetOffset();

            int z = 0;
            foreach (CouplingsDisplayBase element in display.GetMatchTargets())
            {
                double xPos = element.GetMatch().Margin.Left;
                double yPos = element.GetMatch().Margin.Top + offset[z++] * 60;

                element.GetMatch().MoveTo(xPos, yPos);
            }
        }

        /// <summary> Event which starts the game. </summary>
        private void start(object sender, RoutedEventArgs e)
        {
            setDisplay(engine.GetCouplings());

            (Finder.FindElementWithTag(allControls, "CouplingText") as TextBox).Text = engine.GetCouplingText();

            foreach (CouplingsDisplayBase element in display.GetMatchTargets())
            {
                element.GetMatch().Click += couple;
            }

            GUIElements.Add(display);

            ScoreInterface.Instance.StartTimeControler();

            (sender as Button).IsEnabled = false;
        }

        /// <summary> Evenet which couples two words togeather. </summary>
        private void couple(object sender, RoutedEventArgs e)
        {
            int buttonIndex = getIndexOfButton(sender as Button);
            bool correct = check(display.CurrentMatch().GetContent(), (sender as Button).Content.ToString());

            update(correct, buttonIndex);

            nextCoupling();
        }

        /// <summary> When all the matches have been evalueted or time has ran out. </summary>
        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();

            display.SetAllColors();
            display.Show();

            orderCouplings();

            GUIElements.Remove(display);

            GameManager.Instance.NextGame();
        }

        /// <summary> Adds points if the couple was correct. </summary>
        public override void Grader()
        {
            ScoreInterface.Instance.ScoreEngine.ChangePoints(valueOfField);
        }
    }
}
