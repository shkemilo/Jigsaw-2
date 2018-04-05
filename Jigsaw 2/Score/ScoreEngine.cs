using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Score
{
    /// <summary>
    /// Engine used for score managment
    /// </summary>
    public class ScoreEngine : Engine
    {
        private int score;

        public ScoreEngine()
        {
            score = 0;
        }

        public int Score { get => score; }

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
    }
}