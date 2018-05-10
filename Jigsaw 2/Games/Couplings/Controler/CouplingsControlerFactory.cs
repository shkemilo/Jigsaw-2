using Jigsaw_2.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Jigsaw_2.Games.Couplings
{
    internal class CouplingsControlerFactory
    {
        public Grid grid;
        private CouplingsEngine engine;

        public CouplingsControlerFactory(Grid grid)
        {
            this.grid = grid;

            engine = new CouplingsEngine();
        }

        public ICouplingsBehavior GetControler()
        {
            return new CouplingsControler(getComposite(), engine, grid);
        }

        private List<CouplingsLeaf> generateLeafs(string[] content, string tag)
        {
            List<CouplingsLeaf> temp = new List<CouplingsLeaf>();

            int z = 0;
            foreach (Button b in Finder.FindVisualChildrenWithTag<Button>(grid, tag))
            {
                temp.Add(new CouplingsLeaf(b, content[z++]));
            }

            return temp;
        }

        private string[] getContent(Tuple<string, string>[] content, int itemSelecter)
        {
            string[] temp = new string[content.Length];

            for (int i = 0; i < content.Length; i++)
            {
                if (itemSelecter == 1)
                {
                    temp[i] = content[i].Item1;
                }
                else if (itemSelecter == 2)
                {
                    temp[i] = content[i].Item2;
                }
            }

            return temp;
        }

        private CouplingsComposite getComposite()
        {
            return new CouplingsComposite(generateLeafs(getContent(engine.GetCouplings(), 1), "Match"), generateLeafs(getContent(engine.GetCouplings(), 2), "MatchTarget"));
        }
    }
}