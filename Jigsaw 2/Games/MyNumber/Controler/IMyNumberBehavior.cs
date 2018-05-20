using System.Threading.Tasks;
using System.Windows.Controls;

namespace Jigsaw_2.Games.MyNumber
{
    internal interface IMyNumberBehavior
    {
        void Add(Button button);

        void Undo(Button button);

        void Uncover();

        void Start();

        Task Submit();
    }
}