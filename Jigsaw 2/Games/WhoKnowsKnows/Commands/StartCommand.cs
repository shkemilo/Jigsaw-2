using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    class StartCommand : ICommand
    {
        private readonly IWKKBehavior wkkBehavior;

        public StartCommand(IWKKBehavior wkkBehavior)
        {
            this.wkkBehavior = wkkBehavior;
        }

        public void Execute()
        {
            wkkBehavior.Start();
        }
    }
}
