using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Couplings
{
    internal class StartCommand : ICommand
    {
        private ICouplingsBehavior couplingsBehavior;

        public StartCommand(ICouplingsBehavior couplingsBehavior)
        {
            this.couplingsBehavior = couplingsBehavior;
        }

        public void Execute()
        {
            couplingsBehavior.Start();
        }
    }
}