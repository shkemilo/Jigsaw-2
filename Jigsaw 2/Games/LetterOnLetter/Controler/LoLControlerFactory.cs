using Jigsaw_2.Abstracts;
using Jigsaw_2.Helpers;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Jigsaw_2.Games.LetterOnLetter
{
    public class LoLControlerFactory
    {
        private readonly Grid grid;

        public LoLControlerFactory(Grid grid)
        {
            this.grid = grid;
        }

        public ILoLGameBehavior GetControler()
        {
            return new LoLGameControler(new LoLGUI(createControler()), new LoLEngine(), grid);
        }

        private List<IAnimatableGUI> createControler()
        {
            List<IAnimatableGUI> tempList = new List<IAnimatableGUI>();

            foreach (Button b in Finder.FindVisualChildrenWithTag<Button>(grid, "CharacterDisplayButton"))
            {
                tempList.Add(new AnimatableDisplay(b));
            }

            return tempList;
        }
    }
}