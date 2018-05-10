using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace Jigsaw_2.Games.Couplings
{
    /// <summary>
    /// Interaction logic for Couplings.xaml
    /// </summary>
    public partial class Couplings : GamePage
    {
        private readonly ICouplingsBehavior couplingsBehavior;

        private readonly ICommand startCommand;

        public Couplings()
        {
            InitializeComponent();

            couplingsBehavior = new CouplingsControlerFactory(CouplingsGrid).GetControler();

            SetGame(couplingsBehavior as Game);

            foreach (Button b in Finder.FindVisualChildrenWithTag<Button>(CouplingsGrid, "MatchTarget"))
            {
                b.Click += coupleHandler;
            }

            startCommand = new StartCommand(couplingsBehavior);
        }

        private void coupleHandler(object sender, RoutedEventArgs e)
        {
            new CoupleCommand(couplingsBehavior, sender as Button).Execute();
        }

        private void startHandler(object sender, RoutedEventArgs e)
        {
            foreach (Button b in Finder.FindVisualChildrenWithTag<Button>(CouplingsGrid, "MatchTarget"))
            {
                b.IsEnabled = true;
            }

            startCommand.Execute();

            (sender as Button).IsEnabled = false;
        }
    }
}