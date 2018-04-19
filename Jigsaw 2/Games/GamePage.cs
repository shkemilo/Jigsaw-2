using Jigsaw_2.Abstracts;
using Jigsaw_2.Score;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Jigsaw_2.Games
{
    /// <summary>
    /// Extension of the Page class, merges a page and a game into one class
    /// </summary>
    public class GamePage : Page
    {
        #region Private Fields

        private readonly DispatcherTimer drawTimer;

        private Game game;

        #endregion Private Fields

        #region Constructors

        public GamePage()
        {
            drawTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 30)
            };
            drawTimer.Tick += drawTick;
        }

        #endregion Constructors

        #region Public Properties

        public Game Game { get => game; }

        #endregion Public Properties

        #region Protected Methods

        /// <summary> Sets the game associated with the page </summary>
        protected void SetGame(Game game)
        {
            this.game = game;

            drawTimer.Start();
        }

        #endregion Protected Methods

        #region Events

        /// <summary> Timer for drawing and animating the game. </summary>
        private void drawTick(object sender, EventArgs e)
        {
            ScoreInterface.Instance.DrawScoreInterface();

            if (game.ToAnimate() != null)
            {
                foreach (IAnimatable a in game.ToAnimate())
                {
                    a.Animate();
                }
            }

            if (game.ToDraw() != null)
            {
                foreach (GUIElement g in game.ToDraw())
                {
                    g.Show();
                }
            }

            if (game.ToRender() != null)
            {
                foreach (IGraphic g in game.ToRender())
                {
                    g.Print();
                }
            }
        }

        #endregion Events
    }
}