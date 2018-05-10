using System.Windows.Controls;

namespace Jigsaw_2.Games.Couplings
{
    internal interface ICouplingsBehavior
    {
        void Start();

        void Couple(Button b);
    }
}