using Jigsaw_2.Abstracts;
using System.Windows.Controls;

namespace Jigsaw_2.MainPage.Commands
{
    internal class SettingsCommand : ICommand
    {
        private readonly IMainPageBehavior mainPageBehavior;

        private readonly Button settingsButton;

        public SettingsCommand(IMainPageBehavior mainPageBehavior, Button settingsButton)
        {
            this.mainPageBehavior = mainPageBehavior;

            this.settingsButton = settingsButton;
        }

        public void Execute()
        {
            mainPageBehavior.Settings(settingsButton);
        }
    }
}