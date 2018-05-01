﻿using System.Collections.Generic;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    public class Question
    {
        private readonly List<string> answers;
        private readonly string questionText;
        private readonly int correctIndex;

        public Question(List<string> answers, string questionText, int correctIndex)
        {
            this.answers = answers;
            this.questionText = questionText;
            this.correctIndex = correctIndex;
        }

        public List<string> Answers => answers;

        public string QuestionText => questionText;

        public int CorrectIndex => correctIndex;
    }
}