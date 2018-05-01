using Jigsaw_2.Abstracts;
using System.Collections.Generic;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    public class WKKEngine : Engine
    {
        private readonly Queue<Question> questions;
        private Question currentQuestion;

        private bool empty;

        private readonly int numberOfFields;

        public WKKEngine(int numberOfFields = 4)
        {
            this.numberOfFields = numberOfFields;

            questions = new QuestionGenerator().Questions;

            currentQuestion = null;
            empty = false;
        }

        public Question Question => currentQuestion;

        public bool Empty { get => empty; }

        public bool Check(string answer)
        {
            return currentQuestion.Answers.IndexOf(answer) == currentQuestion.CorrectIndex;
        }

        public bool[] GetAnswers()
        {
            bool[] temp = new bool[numberOfFields];

            temp[currentQuestion.CorrectIndex] = true;

            return temp;
        }

        public void Next()
        {
            if (questions.Count != 0)
            {
                currentQuestion = questions.Dequeue();
            }
            else
            {
                empty = true;
            }
        }
    }
}