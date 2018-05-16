using Jigsaw_2.Abstracts;
using System.Windows.Controls;

namespace Jigsaw_2.Games.Associations
{
    internal class UncoverCommand : ICommand
    {
        private readonly IAssociationBehavior associationBehavior;

        private Button button;

        public UncoverCommand(IAssociationBehavior associationBehavior, Button button)
        {
            this.associationBehavior = associationBehavior;

            this.button = button;
        }

        public void Execute()
        {
            associationBehavior.Uncover(button);
        }
    }
}