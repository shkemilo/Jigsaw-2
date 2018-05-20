using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Jigsaw_2.Games.MyNumber
{
    internal class MyNumberControlerFactory
    {
        private Grid grid;

        public MyNumberControlerFactory(Grid grid)
        {
            this.grid = grid;
        }

        public IMyNumberBehavior GetControler()
        {
            return new MyNumberControler(getGUI(), new MyNumberEngine(), grid);
        }

        private IAnimatableGUI getGUI()
        {
            List<IAnimatableGUI> temp = new List<IAnimatableGUI>();

            temp.AddRange(getLeafs("TargetNumber", ExpressionGenerator.smallNumbers));
            temp.AddRange(getLeafs("SmallNumber", ExpressionGenerator.smallNumbers));
            temp.AddRange(getLeafs("MediumNumber", ExpressionGenerator.mediumNumbers));
            temp.AddRange(getLeafs("BigNumber", ExpressionGenerator.bigNumbers));

            return new MyNumberComposite(temp);
        }

        private List<IAnimatableGUI> getLeafs(string tag, int[] numberScope)
        {
            List<IAnimatableGUI> temp = new List<IAnimatableGUI>();

            foreach (Button element in Finder.FindVisualChildrenWithTag<Button>(grid, tag))
            {
                temp.Add(new MyNumberLeaf(element, numberScope));
            }

            return temp;
        }
    }
}