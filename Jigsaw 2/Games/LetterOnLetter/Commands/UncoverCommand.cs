using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.LetterOnLetter.Commands
{
    internal class UncoverCommand : ICommand
    {
        private static int count = 0;

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
    }
}