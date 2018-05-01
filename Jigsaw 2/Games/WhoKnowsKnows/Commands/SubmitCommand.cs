using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    internal class SubmitCommand : ICommand
    {
        private IWKKBehavior wkkBehavior;

        private string answer;

        public SubmitCommand(IWKKBehavior wkkBehavior, string answer)
        {
            this.wkkBehavior = wkkBehavior;

            this.answer = answer;
        }

        public void Execute()
        {
            wkkBehavior.Submit(answer);
        }
    }
}