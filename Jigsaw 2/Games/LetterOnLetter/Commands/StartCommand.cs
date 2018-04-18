using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.LetterOnLetter.Commands
{
    internal class StartCommand : ICommand
    {
        private ILoLGameBehavior lolGameBehavior;

        public StartCommand(ILoLGameBehavior lolGameBehavior)
        {
            this.lolGameBehavior = lolGameBehavior;
        }

        public void Execute()
        {
            lolGameBehavior.Start();
        }
    }
}