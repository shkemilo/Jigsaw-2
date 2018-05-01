using System.Collections.Generic;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    internal class QuestionGenerator
    {
        private readonly Queue<Question> questions;

        public QuestionGenerator(int numberOfFields = 4)
        {
            questions = new Queue<Question>();

            List<string> question = new List<string>()
            {
                "Teodor",
                "Bobek",
                "Bobak",
                "Broder"
            };

            string questionText = "WHAT IS BRODER NAME?";

            int correctIndex = 1;

            Question question1 = new Question(question, questionText, correctIndex);

            questions.Enqueue(question1);
            questions.Enqueue(question1);
            questions.Enqueue(question1);
        }

        public Queue<Question> Questions => questions;
    }
}