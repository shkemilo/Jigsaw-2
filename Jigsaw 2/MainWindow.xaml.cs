using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using System.Threading;
using Jigsaw_2.Animators;


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

            Thread.Sleep(2000); //prevents random bugs when reseting the application

            Finder.SetAllControls(Finder.FindVisualChildren<Control>(MainWindowGrid).ToList());

            GameManager.Instance.SetUsername();

            ScoreInterface.Instance.DrawScoreInterface();

            FrameAnimator anim = new FrameAnimator(MainFrame, WidthProperty);
        }

        /// <summary> Sets the theme of the application. </summary>
        private void setAppStyle(Accent style, AppTheme theme)
        {
            ThemeManager.ChangeAppStyle(Application.Current, style, theme);
        }

        /// <summary> Shows the dropdown settings menu. </summary>
        private void SettingsMenu(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        /// <summary> Changes the color theme of the application. </summary>
        private void SelectColor(object sender, SelectionChangedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            string selectedColor = (ColorSelecter.SelectedItem as ComboBoxItem).Content.ToString();

            setAppStyle(ThemeManager.GetAccent(selectedColor), appStyle.Item1);
        }

        /// <summary> Activates night mode. </summary>
        private void NightMode(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            setAppStyle(appStyle.Item2, ThemeManager.GetAppTheme("BaseDark"));
        }

        /// <summary> Activates light mode. </summary>
        private void LightMode(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            setAppStyle(appStyle.Item2, ThemeManager.GetAppTheme("BaseLight"));
        }

        /// <summary> Function used for perserving the aspect ratio while resizing. </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            double aspectRatio = 1.5;

            double percentWidthChange = Math.Abs(sizeInfo.NewSize.Width - sizeInfo.PreviousSize.Width) / sizeInfo.PreviousSize.Width;
            double percentHeightChange = Math.Abs(sizeInfo.NewSize.Height - sizeInfo.PreviousSize.Height) / sizeInfo.PreviousSize.Height;

            if (percentWidthChange > percentHeightChange)
                Height = sizeInfo.NewSize.Width / aspectRatio;
            else
                Width = sizeInfo.NewSize.Height * aspectRatio;

            base.OnRenderSizeChanged(sizeInfo);
        }

        private void ShowInstructions(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.ShowInstructions();
        }
    }
}
