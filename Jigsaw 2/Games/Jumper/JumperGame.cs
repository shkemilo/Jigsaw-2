using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using System.Windows.Shapes;
using System.Windows;

namespace Jigsaw_2.Games.Jumper
{ 
    /// <summary>
    /// Class used for representing the Jumper Game.
    /// </summary>
    public class JumperGame : Game
    {
        JumperEngine engine;
        JumperDisplay mainDisp;

        JumperDisplayComponent correctCombinationDisplay;

        List<Control> userControls;

        Button nextRowButton;

        int numberOfRows;

        int[] answer;

        public JumperGame(JumperEngine engine, Grid gameGrid, int numberOfRows = 6) : base(gameGrid)
        {
            this.engine = engine;

            userControls = Finder.FindElementsWithTag(allControls, "UserControls");
            foreach (Control c in userControls)
            {
                (c as Button).Click += addToAnswer;
                c.IsEnabled = false;
            }

            this.numberOfRows = numberOfRows;

            answer = new int[] { 0, 0, 0, 0 };

            setMainDisp(allControls);
            setCorrectCombinationDisplay(allControls);

            nextRowButton = (Button)Finder.FindElementWithTag(allControls, "NextRowButton");
            nextRowButton.Click += startGame;
            nextRowButton.IsEnabled = true;



            engine.Subscribe(mainDisp);
            GUIElements.Add(mainDisp);
        }

        /// <summary> Starts the game when the button is clicked. </summary>
        private void startGame(object sender, RoutedEventArgs e)
        {
            ScoreInterface.Instance.StartTimeControler();

            (sender as Button).Content = "Next Row";
            foreach (Control c in userControls)
                c.IsEnabled = true;


            nextRowButton.Click -= startGame;
            nextRowButton.Click += nextRow;
            nextRowButton.IsEnabled = false;

            mainDisp.SetActiveRowVisual();
        }

        /// <summary> Sets the display of the correct combination. </summary>
        private void setCorrectCombinationDisplay(List<Control> allControls)
        {
            List<Control> tempElements = Finder.FindElementsWithTag(allControls, "CorrectCombination");
            List<JumperDisplayElement> elementHolder = new List<JumperDisplayElement>();

            foreach (Control c in tempElements)
                elementHolder.Add(new JumperDisplayElement(c as Button));

            correctCombinationDisplay = new JumperDisplayComponent(elementHolder);
        }

        private JumperDisplayComponent getDisplayComponent(string tag)
        {
            List<JumperDisplayElement> temp = new List<JumperDisplayElement>();

            foreach(Control c in Finder.FindElementsWithTag(allControls, tag))
            {
                (c as Button).Click += undo;
                temp.Add(new JumperDisplayElement(c as Button));
            }

            return new JumperDisplayComponent(temp);
        }

        private JumperCheckerComponent getCheckerComponent(string tag)
        {
            List<JumperCheckerElement> temp = new List<JumperCheckerElement>();

            foreach (Ellipse e in Finder.FindVisualChildren<Ellipse>(gameGrid))
                if (e.Tag != null)
                    if (e.Tag.ToString() == tag)
                        temp.Add(new JumperCheckerElement(e));

            return new JumperCheckerComponent(temp);
        }

        /// <summary> Sets the main dispaly of the game. </summary>
        private void setMainDisp(List<Control> allControls)
        {
            List<JumperDisplayComponent> mainDispList = new List<JumperDisplayComponent>();
            List<JumperCheckerComponent> mainCheckList = new List<JumperCheckerComponent>();

            for (int i = 0; i < numberOfRows; i++)
            { 
                mainDispList.Add(getDisplayComponent( "JumperDisplay" + (i + 1).ToString() ) );
                mainCheckList.Add(getCheckerComponent( "JumperFeedback" + (i + 1).ToString() ) );
            }

            mainDisp = new JumperDisplay(mainDispList, mainCheckList);
        }

        /// <summary> Adds a element to the answer. </summary>
        private void addToAnswer(object sender, RoutedEventArgs e)
        {
            if (mainDisp.GetCurrentRow().CurrentElementIndex != -1)
                mainDisp.GetActiveElement().SetEnabled(false);

            mainDisp.GetCurrentRow().NextElement();

            answer[mainDisp.GetCurrentRow().CurrentElementIndex] = userControls.IndexOf((sender as Control)) + 1;
            engine.Broadcast(IntToImageConverter.Instance.Convert(answer[mainDisp.GetCurrentRow().CurrentElementIndex]));

            mainDisp.GetActiveElement().SetEnabled(true);

            if (mainDisp.GetCurrentRow().CurrentElementIndex == 3)
                nextRowButton.IsEnabled = true;
        }

        /// <summary> Changes to the next row. </summary> //TODO: This function works but is very very shitty. Refactor it.
        private void nextRow(object sender, RoutedEventArgs e)
        {
            if (mainDisp.CurrentRow < numberOfRows - 1)
            {
                mainDisp.GetActiveElement().SetEnabled(false);

                engine.Broadcast(engine.CheckFeedback(answer));

                if (engine.Check(answer))
                    GameOver();

                mainDisp.ManualChekerShow();
                mainDisp.NextRow();

                mainDisp.SetActiveRowVisual();

                answer = new int[] { 0, 0, 0, 0 };

                nextRowButton.IsEnabled = false;
            }
            else
            {
                engine.Broadcast(engine.CheckFeedback(answer));
                mainDisp.ManualChekerShow();
                mainDisp.GetActiveElement().SetEnabled(false);

                nextRowButton.IsEnabled = false;
                GameOver();
            }
        }

        /// <summary> Undo the last operation. </summary>
        private void undo(object sender, RoutedEventArgs e)
        {
            mainDisp.GetActiveElement().SetEnabled(false);

            answer[mainDisp.GetCurrentRow().CurrentElementIndex] = 0;
            engine.Broadcast(IntToImageConverter.Instance.Convert(answer[mainDisp.GetCurrentRow().CurrentElementIndex]));

            mainDisp.GetCurrentRow().PreviousElement();
            Console.WriteLine(mainDisp.GetCurrentRow().CurrentElementIndex);

            if(mainDisp.GetCurrentRow().CurrentElementIndex != -1)
                mainDisp.GetActiveElement().SetEnabled(true);

            nextRowButton.IsEnabled = false;

        }

        /// <summary> Finishes the game. </summary>
        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();

            foreach (Control c in userControls)
                c.IsEnabled = false;

            GUIElements.Remove(mainDisp);

            for (int i = 0; i < correctCombinationDisplay.NumberOfElements; i++)
            {
                correctCombinationDisplay.NextElement();

                correctCombinationDisplay.Update(IntToImageConverter.Instance.Convert(engine.GetCombination()[i]));
                correctCombinationDisplay.Show();      
            }

            if (engine.Check(answer))
                Grader();

            GameManager.Instance.NextGame();
        }

        /// <summary> Gives points depending on when the combination was solved. </summary>
        public override void Grader()
        {
            ScoreInterface.Instance.ScoreEngine.ChangePoints((numberOfRows - mainDisp.CurrentRow) * 5);
        }
    }

}
