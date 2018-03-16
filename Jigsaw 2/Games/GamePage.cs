using System.Windows.Controls;
using Jigsaw_2.Abstracts;
using System.Windows.Threading;
using System;

namespace Jigsaw_2.Games
{
    public class GamePage : Page
    {
        DispatcherTimer drawTimer;
        
        Game game;

        public GamePage()
        {
            drawTimer = new DispatcherTimer();
            drawTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            drawTimer.Tick += drawTick;
        }

        public Game Game { get => game; }

        protected void SetGame(Game game)
        {
            this.game = game;

            drawTimer.Start();
        }

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
