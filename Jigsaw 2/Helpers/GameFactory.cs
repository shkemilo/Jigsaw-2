using Jigsaw_2.Abstracts;
using Jigsaw_2.Games;
using Jigsaw_2.Games.Couplings;
using Jigsaw_2.Games.Jumper;
using Jigsaw_2.Games.LetterOnLetter;
using Jigsaw_2.Games.WhoKnowsKnows;

namespace Jigsaw_2.Helpers
{
    /// <summary>
    /// Static class that represents a factory for creating Games and their associated pages
    /// </summary>
    public class GameFactory : AbstractFactory
    {
        #region Public Methods

        /// <summary> Creates and returns a GamePage based on the games name. </summary>
        public override GamePage GetGame(string game)
        {
            game = game.ToLower();

            if (game == "letteronletter")
            {
                return new LetterOnLetter();
            }
            else if (game == "jumper")
            {
                return new Jumper();
            }
            else if (game == "couplings")
            {
                return new Couplings();
            }
            else if (game == "whoknowsknows")
            {
                return new WhoKnowsKnows();
            }
            else
            {
                return null;
            }
        }

        public override string GetInstructions(string game)
        {
            return null;
        }

        #endregion Public Methods
    }
}