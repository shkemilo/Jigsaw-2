using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Associations
{
    internal class QuitCommand : ICommand
    {
        private readonly IAssociationBehavior associationBehavior;

        public QuitCommand(IAssociationBehavior associationBehavior)
        {
            this.associationBehavior = associationBehavior;
        }

        public void Execute()
        {
            associationBehavior.Quit();
        }
    }
}