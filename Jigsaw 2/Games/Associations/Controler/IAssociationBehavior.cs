using System.Windows.Controls;

namespace Jigsaw_2.Games.Associations
{
    internal interface IAssociationBehavior
    {
        void Uncover(Button button);

        void Guess(Button button);

        void Quit();

        void Start();
    }
}