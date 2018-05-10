using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    internal class WKKControlerFactory
    {
        public Grid grid;

        public WKKControlerFactory(Grid grid)
        {
            this.grid = grid;
        }

        public IWKKBehavior GetControler()
        {
            return new WKKControler(getGUI(), new WKKEngine(), grid);
        }

        private IGUI getGUI()
        {
            List<IGUI> tempList = new List<IGUI>();

            foreach (Button b in Finder.FindVisualChildrenWithTag<Button>(grid, "QuestionButton"))
            {
                tempList.Add(new QuestionDisplay(b));
            }

            return new QuestionComposite(tempList);
        }
    }
}