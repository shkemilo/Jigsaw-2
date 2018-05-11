using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Jigsaw_2.MainPage
{
    internal sealed class DialogManager
    {
        #region Private Static Fields

        private static DialogManager instance = null;
        private static readonly object padlock = new object();

        #endregion Private Static Fields

        #region Private Fields

        private MainWindow main;

        private AbstractFactory instructionFactory;

        private string username;

        #endregion Private Fields

        #region Constructors

        private DialogManager()
        {
            main = Application.Current.MainWindow as MainWindow;

            instructionFactory = FactoryProducer.GetFactory("instruction");

            string username = string.Empty;
        }

        #endregion Constructors

        #region Public Static Properties

        public static DialogManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DialogManager();
                    }
                    return instance;
                }
            }
        }

        #endregion Public Static Properties

        #region Async

        public async Task ShowInstructions(string gameName)
        {
            await main.ShowMessageAsync("Instructions", instructionFactory.GetInstructions(gameName));
        }

        /// <summary> Gets the users interface </summary>
        public async Task SetUsername(string message = "Enter your username: ")
        {
            string result = await main.ShowInputAsync("Jigsaw", message);

            if (result == null)
            {
                await exitDialog().ConfigureAwait(false);
            }
            else
            {
                result = result.Trim();

                if (badInput(result) != string.Empty)
                {
                    message = badInput(result);
                    await SetUsername(message).ConfigureAwait(false); ;
                }
                else
                {
                    username = result;
                    setCurrentUser();
                }
            }
        }

        /// <summary> Set of commands to be run when there are no games left. </summary>
        public async Task TheEnd()
        {
            MessageDialogResult exitResult = await main.ShowMessageAsync("Jigsaw", "Your score is: " + ScoreInterface.Instance.ScoreEngine.Score + "\n Play Again?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }

            Application.Current.Shutdown();
        }

        /// <summary> Asks the user if he wants to exit the application. </summary>
        private async Task exitDialog()
        {
            MessageDialogResult exitResult = await main.ShowMessageAsync("Jigsaw", "Do you want to exit the game?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings { NegativeButtonText = "No", AffirmativeButtonText = "Yes" });

            if (exitResult == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }
            else
            {
                await SetUsername().ConfigureAwait(false);
            }
        }

        #endregion Async

        #region Private Methods

        /// <summary> Checks if the username was correctly typed. </summary>
        private string badInput(string input)
        {
            string message = string.Empty;

            if (input == string.Empty)
            {
                message = "Username mustn't be blank. Try again.";
            }
            else if (input.Length > 16)
            {
                message = "Username too long. Try again";
            }

            return message;
        }

        /// <summary> Sets the text in the main window to show the current user. </summary>
        private void setCurrentUser()
        {
            foreach (TextBlock tb in Finder.FindVisualChildren<TextBlock>(main))
            {
                if (tb.Tag != null)
                {
                    if (tb.Tag.ToString() == "CurrentUser")
                    {
                        tb.Text = username;
                        break;
                    }
                }
            }
        }

        #endregion Private Methods
    }
}