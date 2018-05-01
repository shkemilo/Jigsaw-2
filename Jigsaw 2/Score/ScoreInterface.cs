using Jigsaw_2.Games;
using Jigsaw_2.Animators;
using Jigsaw_2.Helpers;
using System;
using System.Windows.Controls;
using System.Windows.Threading;
using Jigsaw_2.MainPage;

namespace Jigsaw_2.Score
{
    /// <summary>
    /// Singleton that is used for combining the Score Engine with its GUI.
    /// </summary>
    public sealed class ScoreInterface
    {
        #region Private Static Fields

        private static ScoreInterface instance = null;
        private static readonly object padlock = new object();

        #endregion Private Static Fields

        #region Private Fields

        private ScoreEngine scoreEngine;
        public ScoreEngine ScoreEngine { get => scoreEngine; }

        private Display scoreDisplay;

        private ProgressBar progressBar;

        private DispatcherTimer timeControler;

        #endregion Private Fields

        #region Constructors

        private ScoreInterface()
        {
            scoreEngine = new ScoreEngine();
            scoreDisplay = new Display((TextBox)Finder.FindElementWithTag("ScoreDisplay"));
            progressBar = (ProgressBar)Finder.FindElementWithTag("TimeBar");
            progressBar.Maximum = 120;

            scoreEngine.Subscribe(scoreDisplay);

            scoreEngine.Broadcast(scoreEngine.GetScore());

            timeControler = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timeControler.Tick += timeControlerTick;
        }

        #endregion Constructors

        #region Public Static Properties

        public static ScoreInterface Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ScoreInterface();
                    }
                    return instance;
                }
            }
        }

        #endregion Public Static Properties

        #region Public Methods

        public void SetTimeControlerInterval(TimeSpan time)
        {
            timeControler.Interval = time;
        }

        /// <summary> Draws the score interface GUI elements. </summary>
        public void DrawScoreInterface()
        {
            scoreDisplay.Show();
        }

        /// <summary> Starts time. </summary>
        public void StartTimeControler()
        {
            timeControler.Start();
        }

        /// <summary> Stops time. </summary>
        public void StopTimeControler()
        {
            timeControler.Stop();
        }

        /// <summary> Resets the time bar to 0. </summary>
        public void ResetTimeBar()
        {
            progressBar.SetPercent(0, TimeSpan.FromSeconds(3));
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary> When time is up stop the current game. </summary>
        private void stopCurrentGame()
        {
            GameManager.Instance.GetCurrentGame().GameOver();
        }

        /// <summary> Tick event used for simulating time, and animating the progress bar. </summary>
        private void timeControlerTick(object sender, EventArgs e)
        {
            if (progressBar.Value != progressBar.Maximum)
                progressBar.SetPercent(progressBar.Value + 1, timeControler.Interval);
            else
            {
                stopCurrentGame();
                timeControler.Stop();
            }
        }

        #endregion Private Methods
    }
}