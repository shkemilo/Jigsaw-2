using Jigsaw_2.Abstracts;
using Jigsaw_2.Animators;
using Jigsaw_2.Helpers;
using Jigsaw_2.MainPage;
using Jigsaw_2.MainPage.Commands;
using Jigsaw_2.Score;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Media;

namespace Jigsaw_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly ICommand nightModeCommand;
        private readonly ICommand lightModeCommand;
        private readonly ICommand instructionCommand;
        private readonly ICommand startCurrentGameCommand;

        private ICommand settingsCommand;
        private ICommand colorSelectCommand;

        private MainPageControler mainPageControler;

        #region Constructors

        public MainWindow()
        {
            mainPageControler = new MainPageControler();

            SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.UvodnaSpica);
            soundPlayer.Play();

            InitializeComponent();

            Finder.SetAllControls(Finder.FindVisualChildren<Control>(MainWindowGrid).ToList());

            DialogManager.Instance.SetUsername();

            ScoreInterface.Instance.DrawScoreInterface();

            FrameAnimator anim = new FrameAnimator(MainFrame, WidthProperty);

            nightModeCommand = new NightModeCommand(mainPageControler);
            lightModeCommand = new LightModeCommand(mainPageControler);
            instructionCommand = new InstructionsCommand(mainPageControler);
            startCurrentGameCommand = new StartCurrentGameCommand(mainPageControler, MainFrame, StartButton);
        }

        #endregion Constructors

        #region Events

        /// <summary> Shows the drop down settings menu. </summary>
        private void settingsMenu(object sender, RoutedEventArgs e)
        {
            settingsCommand = new SettingsCommand(mainPageControler, sender as Button);

            settingsCommand.Execute();
        }

        /// <summary> Changes the color theme of the application. </summary>
        private void selectColor(object sender, SelectionChangedEventArgs e)
        {
            colorSelectCommand = new ColorSelectCommand(mainPageControler, ColorSelecter.SelectedItem as ComboBoxItem);

            colorSelectCommand.Execute();
        }

        /// <summary> Activates night mode. </summary>
        private void nightMode(object sender, RoutedEventArgs e)
        {
            nightModeCommand.Execute();
        }

        /// <summary> Activates light mode. </summary>
        private void lightMode(object sender, RoutedEventArgs e)
        {
            lightModeCommand.Execute();
        }

        private void showInstructions(object sender, RoutedEventArgs e)
        {
            instructionCommand.Execute();
        }

        private void startCurrentGame(object sender, RoutedEventArgs e)
        {
            startCurrentGameCommand.Execute();
        }

        /// <summary> Function used for preserving the aspect ratio while resizing. </summary>
        /*protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            double aspectRatio = 1.5;

            double percentWidthChange = Math.Abs(sizeInfo.NewSize.Width - sizeInfo.PreviousSize.Width) / sizeInfo.PreviousSize.Width;
            double percentHeightChange = Math.Abs(sizeInfo.NewSize.Height - sizeInfo.PreviousSize.Height) / sizeInfo.PreviousSize.Height;

            if (percentWidthChange > percentHeightChange)
                Height = sizeInfo.NewSize.Width / aspectRatio;
            else
                Width = sizeInfo.NewSize.Height * aspectRatio;

            base.OnRenderSizeChanged(sizeInfo);
        }*/

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            double aspect = 1.5;

            if (sizeInfo.WidthChanged)
            {
                Width = sizeInfo.NewSize.Height * aspect;
            }
            else
            {
                Height = sizeInfo.NewSize.Width / aspect;
            }
        }

        #endregion Events
    }
}