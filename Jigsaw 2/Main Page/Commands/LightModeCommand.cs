using Jigsaw_2.Abstracts;

namespace Jigsaw_2.MainPage.Commands
{
    internal class LightModeCommand : ICommand
    {
        private readonly IMainPageBehavior mainPageBehavior;

        public LightModeCommand(IMainPageBehavior mainPageBehavior)
        {
            this.mainPageBehavior = mainPageBehavior;
        }

        public void Execute()
        {
            mainPageBehavior.LightMode();
        }
    }
}