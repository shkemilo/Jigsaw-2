using Jigsaw_2.Abstracts;
using System.Windows.Controls;

namespace Jigsaw_2.Games.Couplings
{
    internal class CoupleCommand : ICommand
    {
        private readonly ICouplingsBehavior couplingsBehavior;
        private readonly Button b;

        public CoupleCommand(ICouplingsBehavior couplingsBehavior, Button b)
        {
            this.couplingsBehavior = couplingsBehavior;

            this.b = b;
        }

        public void Execute()
        {
            couplingsBehavior.Couple(b);
        }
    }
}