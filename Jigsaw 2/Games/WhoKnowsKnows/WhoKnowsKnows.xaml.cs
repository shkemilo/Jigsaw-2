using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    /// <summary>
    /// Interaction logic for WhoKnowsKnows.xaml
    /// </summary>
    public partial class WhoKnowsKnows : GamePage
    {
        private readonly IWKKBehavior wkkBehavior;

        public WhoKnowsKnows()
        {
            InitializeComponent();

            foreach (Button element in Finder.FindVisualChildrenWithTag<Button>(WKKGrid, "QuestionButton"))
            {
                element.Click += submitHandler;
            }

            wkkBehavior = new WKKControlerFactory(WKKGrid).GetControler();

            SetGame(wkkBehavior as Game);
        }

        private void submitHandler(object sender, RoutedEventArgs e)
        {
            new SubmitCommand(wkkBehavior, (sender as Button).Content.ToString()).Execute();
        }

        private void nextHandler(object sender, RoutedEventArgs e)
        {
            new SubmitCommand(wkkBehavior, string.Empty).Execute();
        }
    }
}