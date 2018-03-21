using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Jigsaw_2.Score;
using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Linq;
using Jigsaw_2.Games;


namespace Jigsaw_2
{
    /// <summary>
    /// Singleton that is used for navigating through multiple Games.
    /// </summary>
    public sealed class GameManager
    {
        private static GameManager instance = null;
        private static readonly object padlock = new object();

        Queue<string> games;

        GamePage currentGame;

        Control gameChanger;

        MainWindow main;

        string username;

        private GameManager()
        {
            games = new Queue<string>();

            games.Enqueue("letteronletter");
            games.Enqueue("letteronletter");

            games.Enqueue("jumper");
            games.Enqueue("jumper");

            gameChanger = Finder.FindElementWithTag("GameChanger");

            (gameChanger as Button).Click += StartCurrentGame;

            main = (Application.Current.MainWindow as MainWindow);

            username = "";

            NextGame();
        }

        /// <summary> Returns the Instance of the GameManager Singleton. </summary>
        public static GameManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GameManager();
                    }
                    return instance;
                }
            }
        }

        public string Username { get => username;  }

        private string badInput(string input)
        {
            string message = string.Empty;

            if (input == string.Empty)
                message = "Username musn't be blank. Try again.";
            else if (input.Length > 12)
                message = "Username too long. Try again";

            return message;
        }

        private void setCurrentUser()
        {
            foreach (TextBlock tb in Finder.FindVisualChildren<TextBlock>(main))
                if (tb.Tag != null)
                    if (tb.Tag.ToString() == "CurrentUser")
                        tb.Text = username;
        }

        private async void exitDialog()
        {
            MessageDialogResult exitResult = await main.ShowMessageAsync("Jigsaw", "Do you want to exit the game?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
                Application.Current.Shutdown();
            else
                SetUsername();
        }

        public async void SetUsername(string message = "Enter your username: ")
        {
            string result = await main.ShowInputAsync("Jigsaw", message);

            if (result == null)
                exitDialog();
            else
            {
                result = result.Trim();

                if (badInput(result) != string.Empty)
                {
                    message = badInput(result);
                    SetUsername(message);
                }
                else
                {
                    username = result;
                    setCurrentUser();
                }
            }
        }

        /// <summary> Set of commands to be run when there are no games left. </summary>
        private async void theEnd()
        {

            MessageDialogResult exitResult = await main.ShowMessageAsync("Jigsaw", "Your score is: " + ScoreInterface.Instance.ScoreEngine.Score + "\n Play Again?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);

            Application.Current.Shutdown();
        }

        /// <summary> Returns the current game in play. </summary>
        public Game GetCurrentGame()
        {
            return currentGame.Game;
        }

        /// <summary> Returns the current page that is being shown. </summary>
        public GamePage GetCurrentPage()
        {
            return currentGame;
        }

        /// <summary> Starts the current game to be played. </summary>
        public void StartCurrentGame(object sender, RoutedEventArgs e)
        {
            Finder.FindVisualChildren<Frame>(main).ToList().First().Navigate(currentGame);

            gameChanger.IsEnabled = false;
        }

        /// <summary> Changes to the next game. </summary>
        public void NextGame()
        {
            ScoreInterface.Instance.ResetTimeBar();

            if (games.Count == 0)
                theEnd();
            else
            {
                gameChanger.IsEnabled = true;

                currentGame = GameFactory.GetGame(games.Dequeue());
            }
        }

    }
}
