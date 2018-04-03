
namespace Jigsaw_2.Games.Couplings
{
    /// <summary>
    /// Interaction logic for Couplings.xaml
    /// </summary>
    public partial class Couplings : GamePage
    {
        public Couplings()
        {
            InitializeComponent();

            SetGame(new CouplingsGame(new CouplingsEngine(), CouplingsGrid));
        }
    }
}
