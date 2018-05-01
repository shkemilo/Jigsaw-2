using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    internal class QuestionComposite : IGUI
    {
        private readonly List<IGUI> questionsDisplay;

        private Question questionData;

        public QuestionComposite(IEnumerable<IGUI> questionsDisplay)
        {
            this.questionsDisplay = new List<IGUI>(questionsDisplay);
        }

        public void Next()
        {
            updateChildren(new Queue<string>(questionData.Answers));
        }

        public void Enable(bool b)
        {
            foreach (IGUI element in questionsDisplay)
            {
                element.Enable(b);
            }
        }

        public void Print()
        {
            foreach (IGUI element in questionsDisplay)
            {
                element.Print();
            }
        }

        public void Update<T>(T message)
        {
            if (message is Question)
            {
                questionData = message as Question;
                Next();
            }
            else if (message is bool[])
            {
                updateChildren(message as bool[]);
            }
            else
            {
                throw new ArgumentException("Invalid parameter. Expected Question.");
            }
        }

        private void updateChildren(Queue<string> questionTexts)
        {
            foreach (IGUI element in questionsDisplay)
            {
                element.Update(questionTexts.Dequeue());
            }
        }

        private void updateChildren(bool[] answers)
        {
            int i = 0;

            foreach (IGUI element in questionsDisplay)
            {
                element.Update(answers[i++]);
            }
        }
    }
}