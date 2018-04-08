using System.Windows.Controls;

namespace Jigsaw_2.MainPage
{
    internal interface IMainPageBehavior
    {
        void Settings(Button settingsButton);

        void ColorSelect(ComboBoxItem comboBoxItem);

        void NightMode();

        void LightMode();

        void Instructions();

        void StartCurrentGame(Frame mainFrame, Button startButton);
    }
}