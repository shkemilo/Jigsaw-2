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
