using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.MyNumber
{
    internal class SubmitCommand : ICommand
    {
        private readonly IMyNumberBehavior myNumberBehavior;

        public SubmitCommand(IMyNumberBehavior myNumberBehavior)
        {
            this.myNumberBehavior = myNumberBehavior;
        }

        public void Execute()
        {
            myNumberBehavior.Submit();
        }
    }
}