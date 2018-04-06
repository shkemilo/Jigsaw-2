using Jigsaw_2.Abstracts;
using Jigsaw_2.Games;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Jigsaw_2
{
    /// <summary>
    /// Singleton that is used for navigating through multiple Games.
    /// </summary>
    public sealed class GameManager
    {
        #region Private Static Fields

        private static GameManager instance = null;
        private static readonly object padlock = new object();

        #endregion Private Static Fields

        #region Private Fields

        private Queue<string> games;

        private GamePage currentGame;

        private Control gameChanger;

        private MainWindow main;

        private string username;

        #endregion Private Fields

        #region Constructors

        private GameManager()
        {
            games = new Queue<string>();

            /*games.Enqueue("letteronletter");
             games.Enqueue("letteronletter");

             games.Enqueue("jumper");
             games.Enqueue("jumper");*/

            games.Enqueue("couplings");
            games.Enqueue("couplings");

            gameChanger = Finder.FindElementWithTag("GameChanger");

            (gameChanger as Button).Click += StartCurrentGame;

            main = Application.Current.MainWindow as MainWindow;

            username = string.Empty;

            NextGame();
        }

        #endregion Constructors

        #region Public Static Methods

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

        #endregion Public Static Methods

        #region Getters

        public string Username { get => username; }

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

        #endregion Getters

        #region Async

        public async void ShowInstructions()
        {
            await main.ShowMessageAsync("Instructions", GameFactory.GetInstructions(currentGame.Game.Name));
        }

        /// <summary> Gets the users interface </summary>
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

        #endregion Async

        #region Events

        /// <summary> Starts the current game to be played. </summary>
        public void StartCurrentGame(object sender, RoutedEventArgs e)
        {
            Finder.FindVisualChildren<Frame>(main).ToList().First().Navigate(currentGame);

            gameChanger.IsEnabled = false;
        }

        #endregion Events

        #region Public Methods

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

        #endregion Public Methods

        #region Private Methods

        #region Async

        /// <summary> Set of commands to be run when there are no games left. </summary>
        private async void theEnd()
        {
            MessageDialogResult exitResult = await main.ShowMessageAsync("Jigsaw", "Your score is: " + ScoreInterface.Instance.ScoreEngine.Score + "\n Play Again?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);

            Application.Current.Shutdown();
        }

        /// <summary> Asks the user if he wants to exit the application. </summary>
        private async void exitDialog()
        {
            MessageDialogResult exitResult = await main.ShowMessageAsync("Jigsaw", "Do you want to exit the game?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
                Application.Current.Shutdown();
            else
                SetUsername();
        }

        #endregion Async

        /// <summary> Checks if the username was correctly typed. </summary>
        private string badInput(string input)
        {
            string message = string.Empty;

            if (input == string.Empty)
                message = "Username mustn't be blank. Try again.";
            else if (input.Length > 16)
                message = "Username too long. Try again";

            return message;
        }

        /// <summary> Sets the text in the main window to show the current user. </summary>
        private void setCurrentUser()
        {
            foreach (TextBlock tb in Finder.FindVisualChildren<TextBlock>(main))
                if (tb.Tag != null)
                    if (tb.Tag.ToString() == "CurrentUser")
                    {
                        tb.Text = username;
                        break;
                    }
        }

        #endregion Private Methods
    }
}