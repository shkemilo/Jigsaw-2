using Jigsaw_2.Abstracts;
using System.Windows.Controls;

namespace Jigsaw_2.Games.LetterOnLetter.Commands
{
    internal class SelectCommand : ICommand, IUndoable
    {
        private readonly ILoLGameBehavior lolGameBehavior;

        private readonly Button selecter;

        public SelectCommand(ILoLGameBehavior lolGameBehavior, Button selecter)
        {
            this.lolGameBehavior = lolGameBehavior;

            this.selecter = selecter;
        }

        public void Execute()
        {
            lolGameBehavior.Select(selecter);
        }

        public void Undo()
        {
            lolGameBehavior.Undo(selecter);
        }
    }
}