using Jigsaw_2.Abstracts;

namespace Jigsaw_2.MainPage.Commands
{
    internal class NightModeCommand : ICommand
    {
        private readonly IMainPageBehavior mainPageBehavior;

        public NightModeCommand(IMainPageBehavior mainPageBehavior)
        {
            this.mainPageBehavior = mainPageBehavior;
        }

        public void Execute()
        {
            mainPageBehavior.NightMode();
        }
    }
}