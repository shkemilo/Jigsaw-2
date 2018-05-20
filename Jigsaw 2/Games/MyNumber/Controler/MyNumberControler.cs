using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using Jigsaw_2.MainPage;
using Jigsaw_2.Score;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Jigsaw_2.Games.MyNumber
{
    internal class MyNumberControler : Game, IMyNumberBehavior
    {
        private MyNumberComposite gui;
        private MyNumberEngine engine;

        private readonly Display expressionDisplay;

        private string expression;

        private readonly int numberOfFields;

        public MyNumberControler(IAnimatableGUI gui, MyNumberEngine engine, Grid grid, int numberOfFields = 9) : base(grid, "mynumber")
        {
            if (gui is MyNumberComposite)
            {
                this.gui = gui as MyNumberComposite;
            }
            else
            {
                throw new ArgumentException("Wrong GUI type");
            }

            this.engine = engine;

            engine.Subscribe(gui);
            engine.Broadcast(engine.GetAllNumbers());

            expression = string.Empty;

            expressionDisplay = new Display(Finder.FindElementWithTag(allControls, "ExpressionDisplay"));

            GUIElements.Add(expressionDisplay);
            graphicalElements.Add(gui);
            anims.Add(gui);

            gui.Enable(false);

            this.numberOfFields = numberOfFields;
        }

        public void Add(Button button)
        {
            expression += button.Content;

            if (button.Tag.ToString().Contains("Number"))
            {
                button.IsEnabled = false;
            }

            expressionDisplay.Update(expression);
        }

        public void Undo(Button button)
        {
            int length = button.Content.ToString().Length;
            expression = expression.Remove(expression.Length - length, length);

            button.IsEnabled = true;

            expressionDisplay.Update(expression);
        }

        public void Start()
        {
            ScoreInterface.Instance.StartTimeControler();

            gui.Enable(true);

            changeSSImage();
        }

        public void Uncover()
        {
            gui.Next();
        }

        public async Task Submit()
        {
            MessageDialogResult exitResult = await (Application.Current.MainWindow as MetroWindow)?.ShowMessageAsync("Jigsaw", "Are you sure you want to submit your current expression?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
            {
                GameOver();
            }
        }

        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();

            disableAllControls();

            (Finder.FindElementWithTag(allControls, "CorrectExpression") as TextBox).Text = engine.CorrectExpression;

            int result = engine.Check(expression);

            if (result != -1)
            {
                expressionDisplay.Update(expression + " = " + engine.Check(expression).ToString());
            }
            else
            {
                expressionDisplay.Update("INVALID EXPRESSION!");
            }

            Grader();

            GameManager.Instance.NextGame();
        }

        public override void Grader()
        {
            int targetDiff = Math.Abs(engine.Target - engine.Check(expression));

            int score = 0;
            if (targetDiff == -1)
            {
                score = 0;
            }
            else if (targetDiff == 0)
            {
                score = 25;
            }
            else if (targetDiff <= 10)
            {
                score = 15;
            }
            else if (targetDiff <= 25)
            {
                score = 10;
            }

            ScoreInterface.Instance.ScoreEngine.ChangePoints(score);
        }

        private void changeSSImage()
        {
            Finder.FindVisualChildWithTag<Rectangle>(gameGrid, "SSImage").OpacityMask = new VisualBrush() { Visual = (Visual)ResourceDictionaryManager.GetResources()["appbar_door_enter"] };
        }
    }
}