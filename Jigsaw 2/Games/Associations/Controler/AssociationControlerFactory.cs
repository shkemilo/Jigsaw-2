using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Jigsaw_2.Games.Associations
{
    internal class AssociationControlerFactory
    {
        private readonly Grid grid;

        public AssociationControlerFactory(Grid grid)
        {
            this.grid = grid;
        }

        public IAssociationBehavior GetControler()
        {
            return new AssociationControler(new AssociationEngine(), setGUI(), grid);
        }

        private IHideableGUI setGUIElement(string tag)
        {
            List<IHideableGUI> temp = new List<IHideableGUI>();

            int i = 1;

            foreach (Button element in Finder.FindVisualChildrenWithTag<Button>(grid, tag))
            {
                string symbol;

                if (i != 5)
                {
                    symbol = tag + i.ToString();
                }
                else
                {
                    symbol = tag;
                }

                temp.Add(new AssociationLeaf(element, symbol));

                i++;
            }

            return new AssociationComposite(temp);
        }

        private List<IHideableGUI> setGUI()
        {
            List<IHideableGUI> temp = new List<IHideableGUI>
            {
                setGUIElement("A"),
                setGUIElement("B"),
                setGUIElement("C"),
                setGUIElement("D")
            };

            return temp;
        }
    }
}