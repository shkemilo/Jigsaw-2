namespace Jigsaw_2.Abstracts
{
    internal interface IUndoable : ICommand
    {
        void Undo();
    }
}