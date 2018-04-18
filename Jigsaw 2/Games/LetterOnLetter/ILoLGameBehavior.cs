using System.Windows.Controls;

namespace Jigsaw_2.Games.LetterOnLetter
{
    internal interface ILoLGameBehavior
    {
        void Select(Button button);

        void Undo(Button button);

        void Feedback();

        void Uncover();

        void Start();

        void Confirm();
    }
}