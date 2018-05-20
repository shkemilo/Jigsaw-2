using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.MyNumber
{
    internal class UncoverCommand : ICommand
    {
        private static int count;

        private readonly IMyNumberBehavior myNumberBehavior;

        public UncoverCommand(IMyNumberBehavior myNumberBehavior)
        {
            this.myNumberBehavior = myNumberBehavior;

            count = 0;
        }

        public static int Count { get => count; }

        public void Execute()
        {
            myNumberBehavior.Uncover();

            count++;
        }
    }
}