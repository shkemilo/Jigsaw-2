using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.LetterOnLetter.Commands
{
    internal class UncoverCommand : ICommand
    {
        private static int count;

        private ILoLGameBehavior lolGameBehavior;

        public UncoverCommand(ILoLGameBehavior lolGameBehavior)
        {
            this.lolGameBehavior = lolGameBehavior;

            count = 0;
        }

        public static int Count { get => count; }

        public void Execute()
        {
            lolGameBehavior.Uncover();

            count++;
        }
    }
}