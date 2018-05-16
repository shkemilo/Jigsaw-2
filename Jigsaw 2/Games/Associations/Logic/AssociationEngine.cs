using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jigsaw_2.Games.Associations
{
    internal class AssociationEngine : Engine
    {
        private readonly List<Association> associations;
        private readonly string finalAnswer;

        public AssociationEngine()
        {
            AssociationGenerator generator = new AssociationGenerator();

            associations = generator.Associations;
            finalAnswer = generator.FinalAnswer;
        }

        public string FinalAnswer => finalAnswer;

        public List<Association> Associations => associations;

        public bool Check(int index, string guess)
        {
            return associations.ElementAt(index).Answer.Equals(guess, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool Check(string guess)
        {
            return finalAnswer.Equals(guess, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}