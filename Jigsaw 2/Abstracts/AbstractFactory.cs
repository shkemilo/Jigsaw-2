using Jigsaw_2.Games;

namespace Jigsaw_2.Abstracts
{
    public abstract class AbstractFactory
    {
        public abstract GamePage GetGame(string game);
        public abstract string GetInstructions(string game);
    }
}
