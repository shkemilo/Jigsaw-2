using Jigsaw_2.Abstracts;
using Jigsaw_2.Animators;
using Jigsaw_2.Helpers;
using Jigsaw_2.MainPage;
using Jigsaw_2.Score;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jigsaw_2.Games.Couplings
{
    internal class CouplingsControler : Game, ICouplingsBehavior
    {
        private readonly CouplingsEngine engine;
        private readonly CouplingsComposite display;

        private readonly int numberOfFields;
        private int currentElement;

        private readonly TextBox instructionDisplay;

        public CouplingsControler(CouplingsComposite display, CouplingsEngine engine, Grid gameGrid, int numberOfFields = 8) : base(gameGrid, "couplings")
        {
            this.engine = engine;
            this.display = display;

            this.numberOfFields = numberOfFields;
            currentElement = 0;

            instructionDisplay = Finder.FindElementWithTag(allControls, "CouplingText") as TextBox;
        }

        public void Couple(Button coupleTarget)
        {
            string target = coupleTarget.Content.ToString();

            int index = engine.GetIndexOfTarget(target);
            bool correct = engine.Check(engine.GetCouplings()[currentElement].Item1, target);

            update(correct, index);

            nextCoupling();
        }

        public void Start()
        {
            instructionDisplay.Text = engine.GetCouplingText();

            graphicalElements.Add(display);

            ScoreInterface.Instance.StartTimeControler();
        }

        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();

            display.EvaluateAll();
            display.Print();
            orderCouplings();
            graphicalElements.Remove(display);
            disableAllControls();

            GameManager.Instance.NextGame();
        }

        public override void Grader()
        {
            ScoreInterface.Instance.ScoreEngine.ChangePoints(5);
        }

        /// <summary> Changes to a new couplings. </summary>
        private void nextCoupling()
        {
            if (currentElement < numberOfFields - 1)
            {
                display.Next();
                currentElement++;
            }
            else
            {
                GameOver();
            }
        }

        private void update(bool correct, int index)
        {
            display.Update(boolToColor(correct));

            if (correct)
            {
                Grader();

                display.Update(new Tuple<int, SolidColorBrush>(index, boolToColor(correct)));
            }

            display.Print();
        }

        private SolidColorBrush boolToColor(bool correct)
        {
            if (correct)
            {
                return CouplingsLeaf.RightColor;
            }
            else
            {
                return CouplingsLeaf.WrongColor;
            }
        }

        private void orderCouplings()
        {
            int[] offset = engine.GetOffset();

            int z = 0;
            foreach (Button b in Finder.FindElementsWithTag(allControls, "MatchTarget"))
            {
                double xPos = b.Margin.Left;
                double yPos = b.Margin.Top + (offset[z++] * 60);

                b.MoveTo(xPos, yPos);
            }
        }
    }
}