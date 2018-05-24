using Jigsaw_2.Abstracts;
using Jigsaw_2.Animators;
using Jigsaw_2.Helpers;
using Jigsaw_2.MainPage;
using Jigsaw_2.MainPage.Commands;
using Jigsaw_2.Score;
using MahApps.Metro.Controls;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Jigsaw_2.Games.MyNumber;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Jigsaw_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private SoundPlayer soundPlayer;

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

            //startSound();

            InitializeComponent();

            loadHighScore();

            Finder.SetAllControls(Finder.FindVisualChildren<Control>(MainWindowGrid).ToList());

            DialogManager.Instance.SetUsername();

            ScoreInterface.Instance.DrawScoreInterface();

            FrameAnimator anim = new FrameAnimator(MainFrame, WidthProperty);

            nightModeCommand = new NightModeCommand(mainPageControler);
            lightModeCommand = new LightModeCommand(mainPageControler);
            instructionCommand = new InstructionsCommand(mainPageControler);
            startCurrentGameCommand = new StartCurrentGameCommand(mainPageControler, MainFrame, StartButton);

            ExpressionGenerator test = new ExpressionGenerator();
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

        private void stopMusic(object sender, RoutedEventArgs e) //please don't stop the music
        {
            soundPlayer.Stop();
        }

        private void startMusic(object sender, RoutedEventArgs e)
        {
            soundPlayer.PlayLooping();
        }

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

        private async Task startSound()
        {
            soundPlayer = new SoundPlayer(Properties.Resources.UvodnaSpica);

            await Task.Run(() => { soundPlayer.PlaySync(); });

            muteBox.IsEnabled = true;

            soundPlayer = new SoundPlayer(Properties.Resources.BackGroundMusic);

            soundPlayer.PlayLooping();
        }

        private void loadHighScore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Jigsaw_2.Properties.Settings.JigsawDatabaseConnectionString"].ConnectionString;
            string query = "SELECT Username, Score FROM Scores";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                HighScoreGrid.DataContext = dataTable.DefaultView;
            }
        }
    }
}