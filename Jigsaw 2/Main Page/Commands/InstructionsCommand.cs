using Jigsaw_2.Abstracts;

namespace Jigsaw_2.MainPage.Commands
{
    internal class InstructionsCommand : ICommand
    {
        private IMainPageBehavior mainPageBehavior;

        public InstructionsCommand(IMainPageBehavior mainPageBehavior)
        {
            this.mainPageBehavior = mainPageBehavior;
        }

        public void Execute()
        {
            mainPageBehavior.Instructions();
        }
    }
}