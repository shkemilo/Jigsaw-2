namespace Jigsaw_2.Games
{
    /// <summary>
    /// Static class that represents a factory for creating Games and their associated pages
    /// </summary>
    public static class GameFactory
    {
        /// <summary> Creates and returns a GamePage based on the games name. </summary>
        public static GamePage GetGame(string s)
        {
            s = s.ToLower();
            if (s == "letteronletter")
                return new LetterOnLetter.LetterOnLetter();
            else if (s == "jumper")
                return new Jumper.Jumper();

            throw new System.Exception("Game does not exsist. (yet)");
        }
    }
}
