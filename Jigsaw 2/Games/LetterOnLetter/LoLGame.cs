using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Jigsaw_2.Games.LetterOnLetter
{
    /// <summary>
    /// Class used for controlling a Letter on Letter Game.
    /// </summary>
    public class LoLGame : Game // TODO: Needs major refactoring.
    {
        #region Private Fields

        private LoLEngine engine;
        private LoLDisplay mainDisp;

        private Button submit;
        private Button undo;
        private Button check;

        private List<Control> letterDisp;
        private List<Control> usedLetters;

        private Display currentWordDisplay;
        private Display longestWordDisplay;

        private TextBox checkBox;

        private string answer;

        private string state;

        #endregion Private Fields

        #region Constructors

        public LoLGame(LoLEngine engine, Grid gameGrid) : base(gameGrid, "letteronletter")
        {
            letterDisp = Finder.FindElementsWithTag(allControls, "CharacterDisplayButton");

            setLetterDisplayEnabled(false);
            foreach (Control b in letterDisp)
            {
                (b as Button).Click += addToAnswer;
            }

            usedLetters = new List<Control>();

            currentWordDisplay = new Display(Finder.FindElementWithTag(allControls, "CurrentWord"));
            longestWordDisplay = new Display(Finder.FindElementWithTag(allControls, "LongestWord"));

            mainDisp = new LoLDisplay(letterDisp.ToArray(), letterDisp.Count);

            this.engine = engine;

            engine.Subscribe(mainDisp);

            GUIElements.Add(mainDisp);
            GUIElements.Add(currentWordDisplay);
            GUIElements.Add(longestWordDisplay);

            anims.Add(mainDisp);

            checkBox = (TextBox)Finder.FindElementWithTag(allControls, "CheckFeedback");

            check = (Button)Finder.FindElementWithTag(allControls, "CheckerButton");
            check.Click += wordFeedback;
            check.IsEnabled = false;
            submit = (Button)Finder.FindElementWithTag(allControls, "SSButton");
            submit.Click += ssClick;
            undo = (Button)Finder.FindElementWithTag(allControls, "UndoButton");
            undo.Click += undoLastLetter;
            undo.IsEnabled = false;

            answer = string.Empty;

            state = "stop";

            engine.Broadcast(engine.GetLetters());
        }

        #endregion Constructors

        #region Public Override Methods

        /// <summary> Finishes the game. </summary>
        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();

            checkBox.Text = string.Empty;

            check.IsEnabled = false;
            submit.IsEnabled = false;
            undo.IsEnabled = false;

            setLetterDisplayEnabled(false);

            longestWordDisplay.Update(engine.GetLongestWord());
            longestWordDisplay.Show();

            if (checkBoxUpdate())
                Grader();

            GameManager.Instance.NextGame();
        }

        /// <summary> Gives points based on the length of the word. </summary>
        public override void Grader()
        {
            score += answer.Length * 2;

            if (answer.Length == engine.GetLongestWord().Length)
                score += 6;

            ScoreInterface.Instance.ScoreEngine.ChangePoints(score);
        }

        #endregion Public Override Methods

        #region Events

        /// <summary> Adds a letter to the answer. </summary>
        private void addToAnswer(object sender, RoutedEventArgs e)
        {
            Button currentButton = sender as Button;

            answer += currentButton.Content;
            currentWordDisplay.Update(answer);

            currentButton.IsEnabled = false;
            usedLetters.Add(currentButton);

            checkBox.Text = string.Empty;
        }

        /// <summary> Undoes the last letter added to the answer. </summary>
        private void undoLastLetter(object sender, RoutedEventArgs e)
        {
            if (usedLetters.Count != 0)
            {
                usedLetters.Last().IsEnabled = true;
                usedLetters.RemoveAt(usedLetters.Count - 1);
                answer = answer.Remove(answer.Length - 1);

                currentWordDisplay.Update(answer);
                checkBox.Text = string.Empty;
            }
        }

        /// <summary> Gives information if the current word is a valid word. </summary>
        private void wordFeedback(object sender, RoutedEventArgs e)
        {
            if (mainDisp.Finished())
                checkBoxUpdate();
        }

        /// <summary> Handles the stopping and submitting. </summary>
        private void ssClick(object sender, RoutedEventArgs e)
        {
            if (!mainDisp.Finished())
                mainDisp.UncoverLetter();

            if (mainDisp.Finished())
                if (state == "stop")
                    startWordOnWord();
                else if (state == "submit")
                    submitConfirm();
        }

        #endregion Events

        #region Async

        private async void submitConfirm()
        {
            MessageDialogResult exitResult = await (Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Jigsaw", "Are you sure you want to submit your current word?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
                GameOver();
        }

        #endregion Async

        #region Private Methods

        private void changeSSImage()
        {
            foreach (Rectangle r in Finder.FindVisualChildren<Rectangle>(gameGrid))
                if (r.Tag != null)
                    if (r.Tag.ToString() == "SSImage")
                    {
                        r.OpacityMask = new VisualBrush() { Visual = (Visual)ResourceDictionaryManager.GetResources()["appbar_door_enter"] };
                        break;
                    }
        }

        private void setLetterDisplayEnabled(bool b)
        {
            foreach (Control c in letterDisp)
                c.IsEnabled = b;
        }

        private bool checkBoxUpdate()
        {
            if (engine.Check(answer))
            {
                checkBox.Text = "Ova rec postoji u recniku";
                return true;
            }
            else
            {
                checkBox.Text = "Ova rec ne postoji u recniku";
                return false;
            }
        }

        /// <summary> Starts the game. </summary>
        private void startWordOnWord()
        {
            ScoreInterface.Instance.StartTimeControler();

            state = "submit";

            changeSSImage();

            setLetterDisplayEnabled(true);

            check.IsEnabled = true;
            undo.IsEnabled = true;
        }

        #endregion Private Methods
    }
}