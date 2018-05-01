using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Collections.Generic;
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

            IEnumerable<Button> questionButtons = Finder.FindVisualChildrenWithTag<Button>(WKKGrid, "QuestionButton");
            List<IGUI> tempGUIs = new List<IGUI>();
            foreach (Button element in questionButtons)
            {
                element.Click += submitHandler;

                tempGUIs.Add(new QuestionDisplay(element));
            }

            wkkBehavior = new WKKControler(new QuestionComposite(tempGUIs), new WKKEngine(), WKKGrid);

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