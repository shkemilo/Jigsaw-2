using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro;
using Jigsaw_2.Helpers;
using Jigsaw_2.Score;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Jigsaw_2.Games;
using System.Threading;
using System.Windows.Threading;

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

            System.Threading.Thread.Sleep(2000); //prevents random bugs when reseting the application

            Finder.SetAllControls(Finder.FindVisualChildren<Control>(MainWindowGrid).ToList());

            GameManager.Instance.SetUsername();

            ScoreInterface.Instance.DrawScoreInterface();
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

        //TODO: Put this in FrameAnimator class
        #region FrameAnimator
        private bool _allowDirectNavigation = false;
        private NavigatingCancelEventArgs _navArgs = null;
        private Duration _duration = new Duration(TimeSpan.FromSeconds(0.7));
        private double _oldHeight = 0;

        private void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (MainFrame.Content != null && !_allowDirectNavigation)
            {
                e.Cancel = true;

                _navArgs = e;
                _oldHeight = MainFrame.ActualWidth;

                DoubleAnimation animation0 = new DoubleAnimation();
                animation0.From = MainFrame.ActualWidth;
                animation0.To = 0;
                animation0.Duration = _duration;
                animation0.Completed += SlideCompleted;
                MainFrame.BeginAnimation(WidthProperty, animation0);
            }
            _allowDirectNavigation = false;
        }

        private void SlideCompleted(object sender, EventArgs e)
        {
            _allowDirectNavigation = true;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (_navArgs.Uri == null)
                        MainFrame.Navigate(_navArgs.Content);
                    else
                        MainFrame.Navigate(_navArgs.Uri);
                    break;
                case NavigationMode.Back:
                    MainFrame.GoBack();
                    break;
                case NavigationMode.Forward:
                    MainFrame.GoForward();
                    break;
                case NavigationMode.Refresh:
                    MainFrame.Refresh();
                    break;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new DoubleAnimation();
                    animation0.From = 0;
                    animation0.To = _oldHeight;
                    animation0.Duration = _duration;
                    MainFrame.BeginAnimation(WidthProperty, animation0);
                });
        }
    }
    #endregion
}
