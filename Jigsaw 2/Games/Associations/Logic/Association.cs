using System.Collections.Generic;

namespace Jigsaw_2.Games.Associations
{
    internal class Association
    {
        private readonly List<string> fields;
        private readonly string answer;
        private readonly string answerHiddenSymbol;

        private readonly int numberOfFields;

        public Association(List<string> fields, string answer, string hiddenSymbol = "A", int numberOfFields = 4)
        {
            this.fields = fields;
            this.answer = answer;

            this.numberOfFields = numberOfFields;

            answerHiddenSymbol = hiddenSymbol;
        }

        public string Answer => answer;

        public List<string> Fields => fields;

        public string AnswerHiddenSymbol => answerHiddenSymbol;
    }
}