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
using Jigsaw_2.Animators;
using System.Threading;

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
