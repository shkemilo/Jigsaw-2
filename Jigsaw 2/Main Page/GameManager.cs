using Jigsaw_2.Abstracts;
using Jigsaw_2.Games;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Jigsaw_2.MainPage
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

        private readonly Queue<string> games;

        private GamePage currentGame;

        private readonly Control gameChanger;

        private readonly AbstractFactory gameFactory;

        private readonly string username;

        #endregion Private Fields

        #region Constructors

        private GameManager()
        {
            games = new Queue<string>();

            gameFactory = FactoryProducer.GetFactory("game");

            games.Enqueue("letteronletter");
            games.Enqueue("letteronletter");

            games.Enqueue("jumper");
            games.Enqueue("jumper");

            games.Enqueue("couplings");
            games.Enqueue("couplings");

            gameChanger = Finder.FindElementWithTag("GameChanger");

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

        public string Username => username;

        public string GetCurrentGameName()
        {
            return currentGame.Game.Name;
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

        #endregion Getters

        #region Public Methods

        /// <summary> Changes to the next game. </summary>
        public void NextGame()
        {
            ScoreInterface.Instance.ResetTimeBar();

            if (games.Count == 0)
            {
                DialogManager.Instance.TheEnd();
            }
            else
            {
                gameChanger.IsEnabled = true;

                currentGame = gameFactory.GetGame(games.Dequeue());
            }
        }

        #endregion Public Methods
    }
}