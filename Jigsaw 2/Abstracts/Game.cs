using System.Collections.Generic;
using System.Windows.Controls;

namespace Jigsaw_2.Abstracts
{
    /// <summary>
    /// Superclass for various sub-games.
    /// </summary>
    public abstract class Game
    {
        protected List<Control> allControls;

        protected List<GUIElement> GUIElements;
        protected List<Animateable> anims;

        protected int score;

        public Game()
        { 
            GUIElements = new List<GUIElement>();
            anims = new List<Animateable>();

            score = 0;
        }

        public List<Animateable> ToAnimate() { return anims; }

        public List<GUIElement> ToDraw() { return GUIElements; }

        public int GetScore() { return score; }

        public abstract void Grader();

        public abstract void GameOver();

        /*public void Start()
        {
            gameDisplay.Enabled = true;
            gameDisplay.Visible = true;
        }

        public void DisableGame()
        {
            gameDisplay.Enabled = false;
            gameDisplay.Visible = false;
        }*/

    }
}
