using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Score
{
    /// <summary>
    /// Engine used for score management.
    /// </summary>
    public class ScoreEngine : Engine
    {
        #region Private Fields

        private int score;

        #endregion Private Fields

        #region Constructors

        public ScoreEngine()
        {
            score = 0;
        }

        #endregion Constructors

        #region Public Properties

        public int Score { get => score; }

        #endregion Public Properties

        #region Public Methods

        /// <summary> Adds a number of points to the current score. </summary>
        public void ChangePoints(int n)
        {
            score += n;

            Broadcast(score);
        }

        /// <summary> Returns the current score. </summary>
        public int GetScore()
        {
            return score;
        }

        #endregion Public Methods
    }
}