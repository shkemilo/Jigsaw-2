using Jigsaw_2.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Jigsaw_2.Abstracts
{
    /// <summary>
    /// Superclass for various sub-games.
    /// </summary>
    public abstract class Game
    {
        protected Grid gameGrid;
        protected List<Control> allControls;

        protected List<GUIElement> GUIElements;
        protected List<Animateable> anims;

        protected int score;

        private string name;

        public string Name { get => name; }

        public Game(Grid gameGrid, string name)
        {
            this.gameGrid = gameGrid;
            allControls = Finder.FindVisualChildren<Control>(gameGrid).ToList();

            GUIElements = new List<GUIElement>();
            anims = new List<Animateable>();

            score = 0;

            this.name = name;
        }

        public List<Animateable> ToAnimate()
        {
            return anims;
        }

        public List<GUIElement> ToDraw()
        {
            return GUIElements;
        }

        public int GetScore()
        {
            return score;
        }

        public abstract void Grader();

        public abstract void GameOver();
    }
}