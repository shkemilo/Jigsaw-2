namespace Jigsaw_2.Games.Jumper
{
    /// <summary>
    /// Interaction logic for Jumper.xaml
    /// </summary>
    public partial class Jumper : GamePage
    {
        public Jumper()
        {
            InitializeComponent();

            SetGame(new JumperGame(new JumperEngine(), JumperGrid));
        }
    }
}
