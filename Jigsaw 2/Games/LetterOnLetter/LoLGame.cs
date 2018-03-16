using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Resources;
using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;

namespace Jigsaw_2.Games.LetterOnLetter
{
    /// <summary>
    /// Class used for controling a Letter on Letter Game.
    /// </summary>
    public class LoLGame : Game
    {
        LoLEngine engine;
        LoLDisplay mainDisp;

        Button submit;
        Button undo;
        Button check;

        List<Control> letterDisp;
        List<Control> usedLetters;

        Display currentWordDisplay;
        Display longestWordDisplay;

        TextBox checkBox;

        string answer;

        string state;

        Grid gameGrid;

        public LoLGame(LoLEngine engine, Grid gameGrid)
        {
            this.gameGrid = gameGrid;
            allControls = Finder.FindVisualChildren<Control>(gameGrid).ToList();
            letterDisp = Finder.FindElementsWithTag(allControls, "CharacterDisplayButton");
            foreach (Control b in letterDisp)
            {
      
                (b as Button).Click += addToAnswer;
                b.IsEnabled = false;
            }
            usedLetters = new List<Control>();

            currentWordDisplay = new Display(Finder.FindElementWithTag(allControls, "CurrentWord"));
            longestWordDisplay = new Display(Finder.FindElementWithTag(allControls, "LongestWord"));

            mainDisp = new LoLDisplay(letterDisp.ToArray(), letterDisp.Count);

            this.engine = engine;

            engine.Subscribe(mainDisp);

            GUIElements.Add(mainDisp); GUIElements.Add(currentWordDisplay); GUIElements.Add(longestWordDisplay);

            anims.Add(mainDisp);

            checkBox = (TextBox)Finder.FindElementWithTag(allControls, "CheckFeedback");

            check = (Button)Finder.FindElementWithTag(allControls, "CheckerButton");
            check.Click += wordFeedback;
            submit = (Button)Finder.FindElementWithTag(allControls, "SSButton");
            submit.Click += ssClick;
            undo = (Button)Finder.FindElementWithTag(allControls, "UndoButton");
            undo.Click += undoLastLetter;

            answer = "";

            state = "stop";

            engine.Broadcast(engine.GetLetters());
        }

        /// <summary> Adds a letter to the answer. </summary>
        void addToAnswer(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button currentButton = (Button)sender;
                if (currentButton.IsEnabled == true)
                {
                    answer += currentButton.Content;
                    currentWordDisplay.Update(answer);

                    currentButton.IsEnabled = false;

                    usedLetters.Add(currentButton);
                    checkBox.Text = "";
                }
            }
        }

        /// <summary> Undos the last letter added to the answer. </summary>
        void undoLastLetter(object sender, RoutedEventArgs e)
        {
            if (usedLetters.Count != 0)
            {
                usedLetters.Last().IsEnabled = true;
                usedLetters.RemoveAt(usedLetters.Count - 1);
                answer = answer.Remove(answer.Length - 1);

                currentWordDisplay.Update(answer);
                checkBox.Text = "";
            }
        }

        /// <summary> Gives information if the current word is a valid word. </summary>
        void wordFeedback(object sender, RoutedEventArgs e)
        {
            if (mainDisp.Finished())
                if (engine.Check(answer))
                    checkBox.Text = "Ova rec postoji u recniku";
                else
                    checkBox.Text = "Ova rec ne postoji u recniku";
        }

        /// <summary> Handles the stopping and submiting. </summary>
        void ssClick(object sender, RoutedEventArgs e)
        {
            if (!mainDisp.Finished())
                mainDisp.UncoverLetter();

            if (mainDisp.Finished())
                if (state == "stop")
                    startWordOnWord();
                else if (state == "submit")
                    GameOver();
        }

        /// <summary> Starts the game. </summary>
        void startWordOnWord()
        {
            ScoreInterface.Instance.StartTimeControler();

            state = "submit";

            foreach (Rectangle r in Finder.FindVisualChildren<Rectangle>(gameGrid))
                if (r.Tag.ToString() == "SSImage")
                {
                    r.OpacityMask = new VisualBrush() { Visual = (Visual)ResourceDictionaryManager.GetResources()["appbar_check"] };
                    break;
                }

            foreach (Control c in letterDisp)
                c.IsEnabled = true;
        }

        /// <summary> Finishes the game. </summary>
        public override void GameOver()
        {
            ScoreInterface.Instance.StopTimeControler();
            checkBox.Text = "";
            check.IsEnabled = false;
            submit.IsEnabled = false;
            undo.IsEnabled = false;

            Console.WriteLine("sve sam ovo uradio");

            foreach (Control c in letterDisp)
                c.IsEnabled = false;

            longestWordDisplay.Update(engine.GetLongestWord());
            longestWordDisplay.Show();

            if (engine.Check(answer))
            {
                checkBox.Text = "Ova rec postoji u recniku";

                Grader();
            }
            else
            {
                checkBox.Text = "Ova rec ne postoji u recniku";
            }

            GameManager.Instance.NextGame();
        }

        /// <summary> Gives points based on the length of the word. </summary>
        public override void Grader()
        {
            score += answer.Length * 2;

            if (answer.Length == engine.GetLongestWord().Length)
                score += 6;

            ScoreInterface.Instance.ScoreEngine.ChangePoints(score);
            ScoreInterface.Instance.DrawScoreInterface();
        }
    }

}
