using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.LetterOnLetter.Commands
{
    internal class FeedbackCommand : ICommand
    {
        private ILoLGameBehavior lolGameBehavior;

        public FeedbackCommand(ILoLGameBehavior lolGameBehavior)
        {
            this.lolGameBehavior = lolGameBehavior;
        }

        public void Execute()
        {
            lolGameBehavior.Feedback();
        }
    }
}