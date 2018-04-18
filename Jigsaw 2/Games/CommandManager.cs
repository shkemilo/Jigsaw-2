using Jigsaw_2.Abstracts;
using System.Collections.Generic;

namespace Jigsaw_2.Games
{
    internal class CommandManager
    {
        private Stack<IUndoable> commandStack = new Stack<IUndoable>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();

            if (command is IUndoable)
            {
                commandStack.Push(command as IUndoable);
            }
        }

        public void Undo()
        {
            if (commandStack.Count > 0)
            {
                commandStack.Pop().Undo();
            }
        }
    }
}