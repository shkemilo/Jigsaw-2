using Jigsaw_2.Abstracts;
using System.Collections.Generic;

namespace Jigsaw_2.Games.Associations
{
    internal class AssociationGenerator : Generator
    {
        private readonly List<Association> associations;
        private readonly string finalAnswer;

        public AssociationGenerator()
        {
            associations = new List<Association>();

            string test = "nesto";

            List<string> test1 = new List<string>
            {
                test,
                test,
                test,
                test
            };

            Association association1 = new Association(test1, test, "A");
            Association association2 = new Association(test1, test, "B");
            Association association3 = new Association(test1, test, "C");
            Association association4 = new Association(test1, test, "D");

            finalAnswer = "nesto";

            associations.Add(association1);
            associations.Add(association2);
            associations.Add(association3);
            associations.Add(association4);
        }

        public string FinalAnswer => finalAnswer;

        public List<Association> Associations => associations;
    }
}