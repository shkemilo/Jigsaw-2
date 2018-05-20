using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.MyNumber
{
    internal class StartCommand : ICommand
    {
        private readonly IMyNumberBehavior myNumberBehavior;

        public StartCommand(IMyNumberBehavior myNumberBehavior)
        {
            this.myNumberBehavior = myNumberBehavior;
        }

        public void Execute()
        {
            myNumberBehavior.Start();
        }
    }
}