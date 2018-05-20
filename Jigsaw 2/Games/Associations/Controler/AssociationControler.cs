using Jigsaw_2.Abstracts;
using Jigsaw_2.MainPage;
using Jigsaw_2.Score;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Jigsaw_2.Helpers;

namespace Jigsaw_2.Games.Associations
{
    internal class AssociationControler : Game, IAssociationBehavior
    {
        private AssociationEngine engine;
        private List<IHideableGUI> gui;

        private readonly int numberOfFields;

        private int[] openedFields;

        private bool[] answeredColumns;

        public AssociationControler(AssociationEngine engine, IEnumerable<IHideableGUI> gui, Grid grid, int numberOfFields = 4) : base(grid, "associations")
        {
            this.engine = engine;
            this.gui = new List<IHideableGUI>(gui);

            this.numberOfFields = numberOfFields;

            openedFields = new int[numberOfFields];

            answeredColumns = new bool[numberOfFields];
        }

        public void Start()
        {
            enableAllControls();

            int i = 0;
            foreach (IHideableGUI element in gui)
            {
                graphicalElements.Add(element);

                element.Update(engine.Associations.ElementAt(i++));
            }

            ScoreInterface.Instance.StartTimeControler();
        }

        public void Uncover(Button button)
        {
            char symbol = button.Content.ToString()[0];
            int index = Convert.ToInt32(button.Content.ToString()[1] - '1');

            gui.ElementAt(symbol - 'A').Uncover(index);

            openedFields[symbol - 'A']++;

            button.IsEnabled = false;
        }

        public async void Guess(Button button)
        {
            char symbol;

            if (button.Content.ToString().Length > 2)
            {
                symbol = ' ';
            }
            else
            {
                symbol = button.Content.ToString()[0];
            }

            string result = await (Application.Current.MainWindow as MetroWindow)?.ShowInputAsync("Jigsaw", "Guess the answer!");

            if (symbol == ' ')
            {
                if (engine.Check(result))
                {
                    GameOver();

                    button.IsEnabled = false;

                    button.Background = Brushes.DarkGreen;

                    return;
                }
            }
            else
            {
                if (engine.Check(symbol - 'A', result))
                {
                    uncoverAll(symbol - 'A');

                    button.IsEnabled = false;

                    return;
                }
            }

            if (result != null)
            {
                await (Application.Current.MainWindow as MetroWindow)?.ShowMessageAsync("Jigsaw", "Wrong Answer!");
            }
        }

        public void Quit()
        {
            GameOver();
        }

        private void uncoverAll(int index)
        {
            if (!answeredColumns[index])
            {
                uncover(index, true);

                grade(index);

                answeredColumns[index] = true;
            }
        }

        private void uncoverAll()
        {
            Grader();

            for (int i = 0; i < numberOfFields; i++)
            {
                uncoverAll(i);
            }
        }

        private void forcedUncover()
        {
            for (int i = 0; i < numberOfFields; i++)
            {
                uncover(i, false);
            }
        }

        private void grade(int index)
        {
            Grader();

            for (int i = 0; i < numberOfFields - openedFields[index]; i++)
            {
                Grader();
            }
        }

        private void uncover(int index, bool notForced)
        {
            gui.ElementAt(index).Uncover();
            gui.ElementAt(index).Enable(false);

            gui.ElementAt(index).Update(notForced);
        }

        public override void Grader()
        {
            ScoreInterface.Instance.ScoreEngine.ChangePoints(3);
        }

        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();

            forcedUncover();

            (Finder.FindElementWithTag(allControls, "final") as Button).Content = engine.FinalAnswer;

            disableAllControls();

            GameManager.Instance.NextGame();
        }
    }
}