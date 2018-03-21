using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro;
using Jigsaw_2.Abstracts;
using Jigsaw_2.Games.LetterOnLetter;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using System.Windows.Threading;
using System.Collections.Generic;
using Jigsaw_2.Games;
using Jigsaw_2.Games.Jumper;

namespace Jigsaw_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Threading.Thread.Sleep(2000);

            Finder.SetAllControls(Finder.FindVisualChildren<Control>(MainWindowGrid).ToList());

            GameManager.Instance.SetUsername();

            ScoreInterface.Instance.DrawScoreInterface();
            
        }

        private void setAppStyle(Accent style, AppTheme theme)
        {
            ThemeManager.ChangeAppStyle(Application.Current, style, theme);
        }

        private void SettingsMenu(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void SelectColor(object sender, SelectionChangedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            string selectedColor = (ColorSelecter.SelectedItem as ComboBoxItem).Content.ToString();


            setAppStyle(ThemeManager.GetAccent(selectedColor), appStyle.Item1);
        }

        private void NightMode(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            setAppStyle(appStyle.Item2, ThemeManager.GetAppTheme("BaseDark"));

        }

        private void LightMode(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            setAppStyle(appStyle.Item2, ThemeManager.GetAppTheme("BaseLight"));
        }
    }
}
