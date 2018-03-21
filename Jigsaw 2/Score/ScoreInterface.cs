using System;
using System.Windows.Controls;
using System.Windows.Threading;
using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;

namespace Jigsaw_2.Score
{
    /// <summary>
    /// Singleton that is used for combining the Score Engine with its GUI.
    /// </summary>
    public sealed class ScoreInterface
    {
        private static ScoreInterface instance = null;
        private static readonly object padlock = new object();

        ScoreEngine scoreEngine;
        public ScoreEngine ScoreEngine { get => scoreEngine; }

        Display scoreDisplay;
        
        ProgressBar progressBar;

        DispatcherTimer timeControler;

        private ScoreInterface()
        {
            scoreEngine = new ScoreEngine();
            scoreDisplay = new Display((TextBox)Finder.FindElementWithTag("ScoreDisplay"));
            progressBar = (ProgressBar)Finder.FindElementWithTag("TimeBar");
            progressBar.Maximum = 10000;

            scoreEngine.Subscribe(scoreDisplay);

            scoreEngine.Broadcast(scoreEngine.GetScore());

            timeControler = new DispatcherTimer();
            timeControler.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timeControler.Tick += timeControlerTick;
        }

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

        /// <summary> When time is up stop the current game. </summary>
        void stopCurrentGame()
        {
            GameManager.Instance.GetCurrentGame().GameOver();
        }

        /// <summary> Tick event used for simulating time, and animating the progress bar. </summary>
        void timeControlerTick(object sender, EventArgs e)
        {
            if (progressBar.Value != progressBar.Maximum)
                progressBar.Value += 1;
            else
            {
                stopCurrentGame();
                timeControler.Stop();
            }
        }

        /// <summary> Draws the score interface GUI elements. </summary>
        public void DrawScoreInterface()
        {
            scoreDisplay.Show();
        }

        /// <summary> Startstime. </summary>
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
            progressBar.Value = 0;
        }
    }
}
