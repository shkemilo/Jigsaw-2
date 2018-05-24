using Jigsaw_2.Animators;
using Jigsaw_2.Games;
using Jigsaw_2.Helpers;
using Jigsaw_2.MainPage;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Jigsaw_2.Score
{
    /// <summary>
    /// Singleton that is used for combining the Score Engine with its GUI.
    /// </summary>
    public sealed class ScoreInterface
    {
        #region Private Static Fields

        private static readonly object padlock = new object();
        private static ScoreInterface instance = null;

        #endregion Private Static Fields

        #region Private Fields

        private ProgressBar progressBar;
        private Display scoreDisplay;
        private ScoreEngine scoreEngine;
        private DispatcherTimer timeControler;
        public ScoreEngine ScoreEngine { get => scoreEngine; }

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

        /// <summary> Draws the score interface GUI elements. </summary>
        public void DrawScoreInterface()
        {
            scoreDisplay.Show();
        }

        /// <summary> Resets the time bar to 0. </summary>
        public void ResetTimeBar()
        {
            progressBar.SetPercent(0, TimeSpan.FromSeconds(3));
        }

        public void SetTimeControlerInterval(TimeSpan time)
        {
            timeControler.Interval = time;
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

        public void SubmitScore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Jigsaw_2.Properties.Settings.JigsawDatabaseConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "INSERT INTO Scores(Username, score) VALUES(@name, @score)";

                command.Parameters.AddWithValue("@name", DialogManager.Instance.Username);
                command.Parameters.AddWithValue("@score", ScoreEngine.Score);

                command.ExecuteNonQuery();
            }
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
            {
                progressBar.SetPercent(progressBar.Value + 1, timeControler.Interval);
            }
            else
            {
                stopCurrentGame();
                timeControler.Stop();
            }
        }

        #endregion Private Methods
    }
}