using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Jigsaw_2.Games.Jumper
{
    /// <summary>
    /// Class used for representing the Jumper Game.
    /// </summary>
    public class JumperGame : Game
    {
        #region Private Fields

        private int[] answer;
        private JumperDisplayComposite correctCombinationDisplay;
        private JumperEngine engine;
        private JumperDisplay mainDisp;
        private Button nextRowButton;
        private int numberOfRows;
        private List<Control> userControls;

        #endregion Private Fields

        #region Constructors

        public JumperGame(JumperEngine engine, Grid gameGrid, int numberOfRows = 6) : base(gameGrid, "jumper")
        {
            this.engine = engine;

            userControls = Finder.FindElementsWithTag(allControls, "UserControls");

            setUserControlsEnabled(false);
            foreach (Control c in userControls)
            {
                (c as Button).Click += addToAnswer;
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

        #endregion Constructors

        #region Public Override Methods

        /// <summary> Finishes the game. </summary>
        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();

            setUserControlsEnabled(false);

            GUIElements.Remove(mainDisp);

            showCorrectCombinationDisplay();

            Grader();

            GameManager.Instance.NextGame();
        }

        /// <summary> Gives points depending on when the combination was solved. </summary>
        public override void Grader()
        {
            if (engine.Check(answer))
                ScoreInterface.Instance.ScoreEngine.ChangePoints((numberOfRows - mainDisp.CurrentRow) * 5);
        }

        #endregion Public Override Methods

        #region Events

        /// <summary> Adds a element to the answer. </summary>
        private void addToAnswer(object sender, RoutedEventArgs e)
        {
            if (mainDisp.GetCurrentRow().CurrentElementIndex != 3)
            {
                disableField();

                mainDisp.GetCurrentRow().NextElement();

                changeAnswer(userControls.IndexOf(sender as Control) + 1);

                enableField();
            }

            if (mainDisp.GetCurrentRow().CurrentElementIndex == 3)
                nextRowButton.IsEnabled = true;
        }

        /// <summary> Changes to the next row. </summary>
        private void nextRow(object sender, RoutedEventArgs e)
        {
            mainDisp.GetActiveElement().GetField().Click -= undo;

            engine.Broadcast(engine.CheckFeedback(answer));
            mainDisp.Show();

            nextRowButton.IsEnabled = false;

            if (engine.Check(answer) || (mainDisp.CurrentRow == (numberOfRows - 1)))
                GameOver();
            else
            {
                answer = new int[] { 0, 0, 0, 0 };

                mainDisp.NextRow();
                mainDisp.SetActiveRowVisual();
            }
        }

        /// <summary> Starts the game when the button is clicked. </summary>
        private void startGame(object sender, RoutedEventArgs e)
        {
            ScoreInterface.Instance.StartTimeControler();

            Finder.FindVisualChildren<Rectangle>(sender as Button).First().OpacityMask = new VisualBrush() { Visual = (Visual)ResourceDictionaryManager.GetResources()["appbar_navigate_next"] };

            setUserControlsEnabled(true);

            nextRowButton.Click -= startGame;
            nextRowButton.Click += nextRow;
            nextRowButton.IsEnabled = false;

            mainDisp.SetActiveRowVisual();
        }

        /// <summary> Undo the last operation. </summary>
        private void undo(object sender, RoutedEventArgs e)
        {
            disableField();

            changeAnswer(0);

            mainDisp.GetCurrentRow().PreviousElement();

            enableField();

            nextRowButton.IsEnabled = false;
        }

        #endregion Events

        #region Private Methods

        /// <summary> Updates the answer. </summary>
        private void changeAnswer(int newElement)
        {
            answer[mainDisp.GetCurrentRow().CurrentElementIndex] = newElement;
            engine.Broadcast(IntToImageConverter.Instance.Convert(answer[mainDisp.GetCurrentRow().CurrentElementIndex]));
        }

        /// <summary> Disables the current field. </summary>
        private void disableField()
        {
            if (mainDisp.GetCurrentRow().CurrentElementIndex != -1)
                mainDisp.GetActiveElement().GetField().Click -= undo;
        }

        /// <summary> Enables the current field. </summary>
        private void enableField()
        {
            if (mainDisp.GetCurrentRow().CurrentElementIndex != -1)
                mainDisp.GetActiveElement().GetField().Click += undo;
        }

        /// <summary> Sets a JumperCheckerComponent by its tag. </summary>
        private JumperCheckerComposite getCheckerComponent(string tag)
        {
            List<JumperCheckerLeaf> temp = new List<JumperCheckerLeaf>();

            foreach (Ellipse e in Finder.FindVisualChildren<Ellipse>(gameGrid))
                if (e.Tag != null)
                    if (e.Tag.ToString() == tag)
                        temp.Add(new JumperCheckerLeaf(e));

            return new JumperCheckerComposite(temp);
        }

        /// <summary> Sets a JumperDisplayComponent by its tag. </summary>
        private JumperDisplayComposite getDisplayComponent(string tag)
        {
            List<JumperDisplayLeaf> temp = new List<JumperDisplayLeaf>();

            foreach (Control c in Finder.FindElementsWithTag(allControls, tag))
                temp.Add(new JumperDisplayLeaf(c as Button));

            return new JumperDisplayComposite(temp);
        }

        /// <summary> Sets the display of the correct combination. </summary>
        private void setCorrectCombinationDisplay(List<Control> allControls)
        {
            List<Control> tempElements = Finder.FindElementsWithTag(allControls, "CorrectCombination");
            List<JumperDisplayLeaf> elementHolder = new List<JumperDisplayLeaf>();

            foreach (Control c in tempElements)
                elementHolder.Add(new JumperDisplayLeaf(c as Button));

            correctCombinationDisplay = new JumperDisplayComposite(elementHolder);
        }

        /// <summary> Sets the main display of the game. </summary>
        private void setMainDisp(List<Control> allControls)
        {
            List<JumperDisplayComposite> mainDispList = new List<JumperDisplayComposite>();
            List<JumperCheckerComposite> mainCheckList = new List<JumperCheckerComposite>();

            for (int i = 0; i < numberOfRows; i++)
            {
                mainDispList.Add(getDisplayComponent("JumperDisplay" + (i + 1).ToString()));
                mainCheckList.Add(getCheckerComponent("JumperFeedback" + (i + 1).ToString()));
            }

            mainDisp = new JumperDisplay(mainDispList, mainCheckList);
        }

        /// <summary> Enabled/Disables the user controls. </summary>
        private void setUserControlsEnabled(bool b)
        {
            foreach (Control c in userControls)
                c.IsEnabled = b;
        }

        /// <summary> Shows the correct combination. </summary>
        private void showCorrectCombinationDisplay()
        {
            for (int i = 0; i < correctCombinationDisplay.NumberOfElements; i++)
            {
                correctCombinationDisplay.NextElement();

                correctCombinationDisplay.Update(IntToImageConverter.Instance.Convert(engine.GetCombination()[i]));
                correctCombinationDisplay.Show();
            }
        }

        #endregion Private Methods
    }
}