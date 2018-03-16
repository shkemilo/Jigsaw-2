using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Jigsaw_2.Score;
using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Linq;
using System.Collections;
using Jigsaw_2.Games;
using Jigsaw_2.Games.LetterOnLetter;
using System.Windows.Navigation;

namespace Jigsaw_2
{
    /// <summary>
    /// Singleton that is used for navigating through multiple Games.
    /// </summary>
    public sealed class GameManager
    {
        private static GameManager instance = null;
        private static readonly object padlock = new object();

        Queue<GamePage> games;
        int currentGame;

        Control gameChanger;

        MainWindow main;

        string username;

        private GameManager()
        {
            currentGame = 0;
            games = null;

            gameChanger = Finder.FindElementWithTag("GameChanger");

            (gameChanger as Button).Click += StartCurrentGame;

            Console.WriteLine(gameChanger.Parent.ToString());

            main = (Application.Current.MainWindow as MainWindow);

            username = "";
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

        public async void SetUsername()
        {
            var result = await main.ShowInputAsync("Jigsaw", "Enter your username: ");

            if (result == null)
            {
                MessageDialogResult exitResult = await main.ShowMessageAsync("Jigsaw", "Do you want to exit the game?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

                if (exitResult == MessageDialogResult.Affirmative)
                    Application.Current.Shutdown();
                else
                    SetUsername();
            }
            else
            {
                if ((result == "") || (result[0] == ' '))
                    SetUsername();

                result.Trim();

                username = result;

                (Finder.FindElementWithTag("CurrentPlayer") as Label).Content += username;
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

        /// <summary> Sets the Games field. </summary>
        public void SetGames(Queue<GamePage> games) { this.games = games; }

        /// <summary> Returns the current game in play. </summary>
        public Game GetCurrentGame()
        {
            if (games != null)
                return games.Peek().Game;

            return null;
        }

        public GamePage GetCurrentPage()
        {
            if (games != null)
                return games.Peek();

            return null;
        }

        /// <summary> Starts the current game to be played. </summary>
        public void StartCurrentGame(object sender, RoutedEventArgs e)
        {
            Finder.FindVisualChildren<Frame>(main).ToList().First().Navigate(games.Peek());

            gameChanger.IsEnabled = false;
        }

        /// <summary> Changes to the next game. </summary>
        public void NextGame()
        {
            ScoreInterface.Instance.ResetTimeBar();

            if (games.Count == 1)
                theEnd();
            else
            {
                gameChanger.IsEnabled = true;

                games.Dequeue();
            }
        }

    }
}
