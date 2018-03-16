using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
