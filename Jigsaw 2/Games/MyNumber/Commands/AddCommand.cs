using Jigsaw_2.Abstracts;
using System.Windows.Controls;

namespace Jigsaw_2.Games.MyNumber
{
    internal class AddCommand : ICommand, IUndoable
    {
        private readonly IMyNumberBehavior myNumberBehavior;

        private readonly Button button;

        public AddCommand(IMyNumberBehavior myNumberBehavior, Button button)
        {
            this.myNumberBehavior = myNumberBehavior;
            this.button = button;
        }

        public void Execute()
        {
            myNumberBehavior.Add(button);
        }

        public void Undo()
        {
            myNumberBehavior.Undo(button);
        }
    }
}