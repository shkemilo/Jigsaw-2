using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.LetterOnLetter.Commands
{
    internal class FeedbackCommand : ICommand
    {
        private readonly ILoLGameBehavior lolGameBehavior;

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