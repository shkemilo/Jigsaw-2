using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.LetterOnLetter.Commands
{
    internal class ConfirmCommand : ICommand
    {
        private readonly ILoLGameBehavior lolGameBehavior;

        public ConfirmCommand(ILoLGameBehavior lolGameBehavior)
        {
            this.lolGameBehavior = lolGameBehavior;
        }

        public void Execute()
        {
            lolGameBehavior.Confirm();
        }
    }
}