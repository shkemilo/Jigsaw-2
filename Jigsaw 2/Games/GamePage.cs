using System.Windows.Controls;
using Jigsaw_2.Abstracts;
using System.Windows.Threading;
using System;

namespace Jigsaw_2.Games
{

    /// <summary>
    /// Extension of the Page class, merges a page and a game into one class
    /// </summary>
    public class GamePage : Page
    {
        DispatcherTimer drawTimer;
        
        Game game;

        public GamePage()
        {
            drawTimer = new DispatcherTimer();
            drawTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            drawTimer.Tick += drawTick;
        }

        public Game Game { get => game; }

        /// <summary> Sets the game associated with the page </summary>
        protected void SetGame(Game game)
        {
            this.game = game;

            drawTimer.Start();
        }

        /// <summary> Timer for drawing and animating the game. </summary>
        private void drawTick(object sender, EventArgs e)
        {
            if (game.ToAnimate() != null)
                foreach (Animateable a in game.ToAnimate())
                    a.Animate();

            if (game.ToDraw() != null)
                foreach (GUIElement g in game.ToDraw())
                    g.Show();
        }

    }
}
