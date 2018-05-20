using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace Jigsaw_2.Games.Associations
{
    /// <summary>
    /// Interaction logic for Association.xaml
    /// </summary>
    public partial class Associations : GamePage
    {
        private readonly IAssociationBehavior associationBehavior;

        private readonly ICommand quitCommand;
        private readonly ICommand startCommand;

        public Associations()
        {
            InitializeComponent();

            associationBehavior = new AssociationControlerFactory(grid).GetControler();

            SetGame(associationBehavior as Game);

            addUncoverHandler("A");
            addUncoverHandler("B");
            addUncoverHandler("C");
            addUncoverHandler("D");

            startCommand = new StartCommand(associationBehavior);
            quitCommand = new QuitCommand(associationBehavior);
        }

        private void uncoverHandler(object sender, RoutedEventArgs e)
        {
            new UncoverCommand(associationBehavior, sender as Button).Execute();
        }

        private void guessHandler(object sender, RoutedEventArgs e)
        {
            new GuessCommand(associationBehavior, sender as Button).Execute();
        }

        private void quitHandler(object sender, RoutedEventArgs e)
        {
            quitCommand.Execute();
        }

        private void startHandler(object sender, RoutedEventArgs e)
        {
            startCommand.Execute();

            (sender as Button).IsEnabled = false;
        }

        private void addUncoverHandler(string tag)
        {
            int i = 0;

            foreach (Button button in Finder.FindVisualChildrenWithTag<Button>(grid, tag))
            {
                if (i != 4)
                {
                    button.Click += uncoverHandler;
                }

                i++;
            }
        }
    }
}