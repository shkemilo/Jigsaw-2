using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Helpers
{
    public static class FactoryProducer
    {
        public static AbstractFactory GetFactory(string factory)
        {
            factory = factory.ToLower();

            if (factory == "game")
            {
                return new GameFactory();
            }
            else if (factory == "instruction")
            {
                return new InstructionFactory();
            }
            else
            {
                return null;
            }
        }
    }
}