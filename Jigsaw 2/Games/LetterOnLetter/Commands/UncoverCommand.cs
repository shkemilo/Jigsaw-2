using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.LetterOnLetter.Commands
{
    internal class UncoverCommand : ICommand
    {
        private static int count;

        private readonly ILoLGameBehavior lolGameBehavior;

        public UncoverCommand(ILoLGameBehavior lolGameBehavior)
        {
            this.lolGameBehavior = lolGameBehavior;
        }

        public void Execute()
        {
            lolGameBehavior.Uncover();

            addCount();
        }

        private static void addCount()
        {
            count++;
        }

        public static int GetCount()
        {
            return count;
        }

        public static void ResetCount()
        {
            count = 0;
        }
    }
}