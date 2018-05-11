using Jigsaw_2.Abstracts;
using System.Windows.Controls;

namespace Jigsaw_2.MainPage.Commands
{
    internal class ColorSelectCommand : ICommand
    {
        private readonly IMainPageBehavior mainPageBehavior;

        private readonly ComboBoxItem comboBoxItem;

        public ColorSelectCommand(IMainPageBehavior mainPageBehavior, ComboBoxItem comboBoxItem)
        {
            this.mainPageBehavior = mainPageBehavior;

            this.comboBoxItem = comboBoxItem;
        }

        public void Execute()
        {
            mainPageBehavior.ColorSelect(comboBoxItem);
        }
    }
}