using MahApps.Metro;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Jigsaw_2.MainPage
{
    internal class MainPageControler : IMainPageBehavior
    {
        #region Public Methods

        public void Settings(Button contextButton)
        {
            contextButton.ContextMenu.IsEnabled = true;
            contextButton.ContextMenu.PlacementTarget = contextButton;
            contextButton.ContextMenu.Placement = PlacementMode.Bottom;
            contextButton.ContextMenu.IsOpen = true;
        }

        public void ColorSelect(ComboBoxItem comboBoxItem)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            string selectedColor = comboBoxItem.Content.ToString();

            setAppStyle(ThemeManager.GetAccent(selectedColor), appStyle.Item1);
        }

        public void NightMode()
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            setAppStyle(appStyle.Item2, ThemeManager.GetAppTheme("BaseDark"));
        }

        public void LightMode()
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            setAppStyle(appStyle.Item2, ThemeManager.GetAppTheme("BaseLight"));
        }

        public void Instructions()
        {
            DialogManager.Instance.ShowInstructions(GameManager.Instance.GetCurrentGameName());
        }

        public void StartCurrentGame(Frame mainFrame, Button startButton)
        {
            mainFrame.Navigate(GameManager.Instance.GetCurrentPage());

            startButton.IsEnabled = false;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary> Sets the theme of the application. </summary>
        private void setAppStyle(Accent style, AppTheme theme)
        {
            ThemeManager.ChangeAppStyle(Application.Current, style, theme);
        }

        #endregion Private Methods
    }
}