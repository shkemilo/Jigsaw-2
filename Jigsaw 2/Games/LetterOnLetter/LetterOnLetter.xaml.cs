namespace Jigsaw_2.Games.LetterOnLetter
{
    /// <summary>
    /// Interaction logic for LetterOnLetter.xaml
    /// </summary>
    public partial class LetterOnLetter : GamePage
    {
        public LetterOnLetter()
        {
            InitializeComponent();

            SetGame(new LoLGame(new LoLEngine(12), LetterOnLetterGrid));
        }
    }
}