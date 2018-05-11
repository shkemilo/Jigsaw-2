using Jigsaw_2.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Jigsaw_2.Abstracts
{
    /// <summary>
    /// Superclass for various sub-games.
    /// </summary>
    public abstract class Game : IGame
    {
        #region Protected Fields

        protected Grid gameGrid;
        protected List<Control> allControls;

        protected List<GUIElement> GUIElements;
        protected List<IAnimatable> anims;
        protected List<IGraphic> graphicalElements;

        protected int score;

        #endregion Protected Fields

        #region Private Fields

        private readonly string name;

        #endregion Private Fields

        #region Constructor

        protected Game(Grid gameGrid, string name)
        {
            this.gameGrid = gameGrid;
            allControls = Finder.FindVisualChildren<Control>(gameGrid).ToList();

            GUIElements = new List<GUIElement>();
            anims = new List<IAnimatable>();
            graphicalElements = new List<IGraphic>();

            score = 0;

            this.name = name;
        }

        #endregion Constructor

        #region Public Properties

        public string Name { get => name; }

        #endregion Public Properties

        #region Public Methods

        public List<IAnimatable> ToAnimate()
        {
            return anims;
        }

        public List<GUIElement> ToDraw()
        {
            return GUIElements;
        }

        public List<IGraphic> ToRender()
        {
            return graphicalElements;
        }

        public int GetScore()
        {
            return score;
        }

        #endregion Public Methods

        #region Public Abstract Methods

        public abstract void Grader();

        public abstract void GameOver();

        #endregion Public Abstract Methods

        protected void disableAllControls()
        {
            foreach (Control control in allControls)
            {
                control.IsEnabled = false;
            }
        }

        protected void enableAllControls()
        {
            foreach (Control control in allControls)
            {
                control.IsEnabled = true;
            }
        }
    }
}