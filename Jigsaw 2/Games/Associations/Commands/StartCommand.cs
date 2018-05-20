using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Associations
{
    internal class StartCommand : ICommand
    {
        private readonly IAssociationBehavior associationBehavior;

        public StartCommand(IAssociationBehavior associationBehavior)
        {
            this.associationBehavior = associationBehavior;
        }

        public void Execute()
        {
            associationBehavior.Start();
        }
    }
}