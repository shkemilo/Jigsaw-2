namespace Jigsaw_2.Games
{
    static class GameFactory
    { 
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
